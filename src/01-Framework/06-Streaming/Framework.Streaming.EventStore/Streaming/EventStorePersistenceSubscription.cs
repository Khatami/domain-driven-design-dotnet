using EventStore.Client;
using Framework.Query.Attributes;
using Framework.Query.Streaming;
using Framework.Streaming.EventStore.Metadata;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Framework.Streaming.EventStore.Streaming
{
	public class EventStorePersistenceSubscription
	{
		private IProjection[] _projections;
		private readonly EventStorePersistentSubscriptionsClient _client;

		public EventStorePersistenceSubscription(EventStorePersistentSubscriptionsClient client,
			IProjection[] projections)
		{
			_client = client;
			_projections = projections;
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
				string groupName = "Marketplace";

				if (subscriptions
					.Where(subscription => subscription.EventSource == streamName)
					.Any(subscription => subscription.GroupName == groupName) == false)
				{
					await _client.CreateToStreamAsync(
						streamName: streamName,
						groupName: groupName,
						settings: subscriptionSettings
					);
				}

				await _client.SubscribeToStreamAsync(
					streamName: streamName,
					groupName: groupName,
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
			var stream = resolvedEvent.Event.EventStreamId.Split("-").First();

			var @event = ReadEvents(resolvedEvent);

			var projection = _projections
				.Where(current => current.GetType().GetCustomAttribute<StreamingAttribute>(true)!.Streams.Contains(stream))
				.ToList();

			await Task.WhenAll(_projections.Select(x => x.Project(@event)));

			await subscription.Ack(resolvedEvent);
		}

		private void SubscriptionDropped(PersistentSubscription subscription, SubscriptionDroppedReason reason, Exception? exception)
		{
			Console.WriteLine($"Subscription dropped: {reason}");
			if (exception != null)
			{
				Console.WriteLine($"Exception: {exception.Message}");
			}
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