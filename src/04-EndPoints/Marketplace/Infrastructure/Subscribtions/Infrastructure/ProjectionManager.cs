using EventStore.Client;
using Marketplace.Streaming.EventStore.Metadata;
using System.Text;
using System.Text.Json;

namespace Marketplace.Infrastructure.Subscribtions.Infrastructure
{
	//********************************************
	// Temperoray
	//********************************************
	public class ProjectionManager
    {
        private readonly EventStoreClient _client;
        private readonly ILogger<ProjectionManager> _logger;

        private IProjection[] _projections;

        public ProjectionManager(EventStoreClient client,
            ILogger<ProjectionManager> logger)
        {
            _client = client;
            _logger = logger;
        }

        public void Start(params IProjection[] projections)
        {
			_projections = projections;

			_client.SubscribeToAllAsync(FromAll.Start, EventAppeared);
        }

        private Task EventAppeared(StreamSubscription subscription,
            ResolvedEvent resolvedEvent,
            CancellationToken cancellationToken)
        {
            if (resolvedEvent.Event.EventType.StartsWith("$"))
                return Task.CompletedTask;

            var @event = ReadEvents(resolvedEvent);

            _logger.LogDebug("Projecting event {evnet}", @event);

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