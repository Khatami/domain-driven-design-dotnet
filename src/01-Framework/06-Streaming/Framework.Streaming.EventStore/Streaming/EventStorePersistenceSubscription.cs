using EventStore.Client;
using Framework.Application.BackgroundJob;
using Framework.Query.Attributes;
using Framework.Query.Streaming;
using Framework.Streaming.EventStore.Metadata;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Framework.Streaming.EventStore.Streaming
{
	public class EventStorePersistenceSubscription
	{
		private readonly EventStorePersistentSubscriptionsClient _client;
		private readonly IProjection[] _projections;
		private readonly IBackgroundJobService _backgroundJobService;
		private readonly string _groupName;

		public EventStorePersistenceSubscription(EventStorePersistentSubscriptionsClient client,
			IProjection[] projections,
			IBackgroundJobService backgroundJobService,
			IConfiguration configuration)
		{
			_client = client;
			_projections = projections;
			_backgroundJobService = backgroundJobService;
			_groupName = configuration.GetSection("ServiceSettings:ServiceName").Get<string>()!;
		}

		public async Task StartAsync()
		{
			var streamingAttributes = _projections
				.Select(projection => projection.GetType().GetCustomAttribute<StreamingAttribute>(true))
				.ToList();

			List<string> streams = new List<string>();
			foreach (var streamingAttribute in streamingAttributes)
			{
				streams.AddRange(streamingAttribute!.Streams);
			}

			var subscriptionSettings = new PersistentSubscriptionSettings(startFrom: StreamPosition.Start,
				resolveLinkTos: true,
				extraStatistics: true);

			var subscriptions = await _client.ListAllAsync();
			foreach (var stream in streams)
			{
				var streamName = "$ce-" + stream;

				if (subscriptions
					.Where(subscription => subscription.EventSource == streamName)
					.Any(subscription => subscription.GroupName == _groupName) == false)
				{
					await _client.CreateToStreamAsync(
						streamName: streamName,
						groupName: _groupName,
						settings: subscriptionSettings
					);
				}

				await _client.SubscribeToStreamAsync(
					streamName: streamName,
					groupName: _groupName,
					eventAppeared: EventAppeared,
					subscriptionDropped: SubscriptionDropped
				);
			}
		}

		private async Task EventAppeared(PersistentSubscription subscription,
			ResolvedEvent resolvedEvent,
			int? retryCount,
			CancellationToken cancellationToken)
		{
			var @event = ReadEvents(resolvedEvent);
			var stream = resolvedEvent.Event.EventStreamId.Split("-").First();
			var eventNumber = resolvedEvent.OriginalEventNumber.ToInt64();
			var version = resolvedEvent.Event.EventNumber.ToInt64();

			var projections = _projections
				.Where(current => current.GetType().GetCustomAttribute<StreamingAttribute>(true)!.Streams.Contains(stream))
				.ToList();

			foreach (var projection in projections)
			{
				_backgroundJobService.Enqueue(BackgroundJobConsts.Inbox,
					() => projection.Project(@event, stream, eventNumber, version));
			}

			await subscription.Ack(resolvedEvent);
		}

		private async void SubscriptionDropped(PersistentSubscription subscription, SubscriptionDroppedReason reason, Exception? exception)
		{
			Console.WriteLine($"Subscription dropped: {reason}");
			if (exception != null)
			{
				Console.WriteLine($"Exception: {exception.Message}");
			}

			await _client.SubscribeToStreamAsync(
				streamName: subscription.SubscriptionId.Split("::").First(),
				groupName: _groupName,
				eventAppeared: EventAppeared,
				subscriptionDropped: SubscriptionDropped
			);
		}

		private object ReadEvents(ResolvedEvent resolvedEvent)
		{
			var metadata = Deserialize<EventMetadata>(resolvedEvent.Event.Metadata.ToArray());

			var dataType = Type.GetType(metadata.ClrType);
			if (dataType == null)
			{
				throw new ArgumentNullException(nameof(dataType));
			}

			var jsonData = Encoding.UTF8.GetString(resolvedEvent.Event.Data.ToArray());

			var data = JsonSerializer.Deserialize(jsonData, dataType);

			if (data == null)
			{
				throw new ArgumentNullException(nameof(data));
			}

			return data;
		}

		private T Deserialize<T>(byte[] data)
		{
			string decodedData = Encoding.UTF8.GetString(data);

			var finalobject = JsonSerializer.Deserialize<T>(decodedData);

			if (finalobject == null)
			{
				throw new ArgumentNullException(nameof(finalobject));
			}

			return finalobject;
		}
	}
}