using EventStore.Client;
using Framework.Query.Streaming;
using Framework.Streaming.EventStore.Metadata;
using System.Text;
using System.Text.Json;

namespace Framework.Streaming.EventStore.Streaming
{
	public class EventStoreProjectionManager
	{
		private readonly EventStoreClient _client;

		private IProjection[] _projections;

		public EventStoreProjectionManager(EventStoreClient client, IProjection[] projections)
		{
			_client = client;

			_projections = projections;
		}

		public void Start()
		{
			_client.SubscribeToAllAsync(FromAll.Start, EventAppeared);
		}

		private Task EventAppeared(StreamSubscription subscription,
			ResolvedEvent resolvedEvent,
			CancellationToken cancellationToken)
		{
			if (resolvedEvent.Event.EventType.StartsWith("$"))
				return Task.CompletedTask;

			var @event = ReadEvents(resolvedEvent);

			return Task.WhenAll(_projections.Select(x => x.Project(@event)));
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