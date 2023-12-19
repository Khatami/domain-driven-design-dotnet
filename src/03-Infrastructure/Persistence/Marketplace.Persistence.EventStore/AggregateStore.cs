using Marketplace.Domain.SeedWork.Aggregation;
using Marketplace.Domain.SeedWork.Streaming;
using System.Text;
using System.Text.Json;

namespace Marketplace.Persistence.EventStore
{
	internal class AggregateStore : IAggregateStore
	{
		private readonly IEventStoreConnection _connection;
		public AggregateStore(IEventStoreConnection connection)
		{
			_connection = connection;
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

			var changes = aggregate.GetChanges().Select(@event => new EventData(
				eventId: Guid.NewGuid(),
				type: @event.GetType().Name,
				isJson: true,
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

			await _connection.AppendToStreamAsync(streamName, aggregate.Version, changes);

			aggregate.ClearChanges();
		}
	}
}
