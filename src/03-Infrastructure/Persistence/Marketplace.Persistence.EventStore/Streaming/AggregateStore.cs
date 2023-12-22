using EventStore.Client;
using Marketplace.Domain.SeedWork.Aggregation;
using Marketplace.Domain.SeedWork.Streaming;
using Marketplace.Persistence.EventStore.Metadata;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace Marketplace.Persistence.EventStore.Streaming
{
    internal class AggregateStore : IAggregateStore
    {
        private readonly EventStoreClient _client;
        public AggregateStore(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("EventStoreConnectionString");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            var settings = EventStoreClientSettings.Create(connectionString);
            _client = new EventStoreClient(settings);
        }

        private static string GetStreamName<T, TId>(TId aggregateId)
        {
            return $"{typeof(T).Name}-{aggregateId!.ToString()}";
        }

        private static string GetStreamName<T, TId>(T aggregate) where T : AggregateRoot<TId>
        {
            return $"{typeof(T).Name}-{aggregate.Id!.ToString()}";
        }

        private static byte[] Serialize(object data)
        {
            return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data));
        }

        public Task<bool> Exists<T, TId>(TId aggregateId)
        {
            throw new NotImplementedException();
        }

        public Task<T> Load<T, TId>(TId aggregateId) where T : AggregateRoot<TId>
        {
            throw new NotImplementedException();
        }

        public async Task Save<T, TId>(T aggregate) where T : AggregateRoot<TId>
        {
            if (aggregate == null)
            {
                throw new ArgumentNullException(nameof(aggregate));
            }

            EventData[] changes = aggregate.GetChanges().Select(@event => new EventData(
                eventId: Uuid.NewUuid(),
                type: @event.GetType().Name,
                contentType: "application/json",
                data: Serialize(@event),
                metadata: Serialize(new EventMetadata
                {
                    ClrType = @event.GetType().AssemblyQualifiedName!
                }))).ToArray();

            if (!changes.Any())
            {
                return;
            }

            var streamName = GetStreamName<T, TId>(aggregate);

            await _client.AppendToStreamAsync(streamName, StreamRevision.FromInt64(aggregate.Version), changes);

            aggregate.ClearChanges();
        }
    }
}