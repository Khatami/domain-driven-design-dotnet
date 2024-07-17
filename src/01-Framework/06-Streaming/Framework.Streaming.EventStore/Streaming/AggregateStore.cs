using EventStore.Client;
using Framework.Application.Streaming;
using Framework.Domain.Aggregation;
using Framework.Streaming.EventStore.Metadata;
using System.Text;
using System.Text.Json;

namespace Framework.Streaming.EventStore.Streaming
{
	internal class AggregateStore : IAggregateStore
	{
		private readonly EventStoreClient _client;

		public AggregateStore(EventStoreClient client)
		{
			_client = client;
		}

		public async Task<T> Load<T, TId>(TId aggregateId) where T : AggregateRoot<TId>
		{
			try
			{
				if (aggregateId == null)
				{
					throw new ArgumentNullException(nameof(aggregateId));
				}

				var stream = GetStreamName<T, TId>(aggregateId);

				var aggregate = (T?)Activator.CreateInstance(typeof(T), true);

				if (aggregate == null)
				{
					throw new ArgumentNullException(nameof(aggregate));
				}

				var events = _client.ReadStreamAsync(Direction.Forwards, stream, StreamPosition.Start);

				object[] history = await ReadEventsAsync(events);

				aggregate.Load(history);

				return aggregate;
			}
			catch (StreamNotFoundException)
			{
				return null!;
			}
		}

		public async Task Save<T, TId>(T aggregate, CancellationToken cancellationToken = default) where T : AggregateRoot<TId>
		{
			if (aggregate == null)
			{
				throw new ArgumentNullException(nameof(aggregate));
			}

			var streamName = GetStreamName<T, TId>(aggregate);

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

			await _client.AppendToStreamAsync(streamName, StreamRevision.FromInt64(aggregate.Version), changes, cancellationToken: cancellationToken);

			aggregate.ClearChanges();
		}

		public async Task Save<TId>(AggregateRoot<TId> aggregate, CancellationToken cancellationToken = default)
		{
			if (aggregate == null)
			{
				throw new ArgumentNullException(nameof(aggregate));
			}

			var streamName = GetStreamName(aggregate);

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

			await _client.AppendToStreamAsync(streamName, StreamRevision.FromInt64(aggregate.Version), changes, cancellationToken: cancellationToken);

			aggregate.ClearChanges();
		}

		public async Task Save(AggregateRootBase aggregate, CancellationToken cancellationToken)
		{
			if (aggregate == null)
			{
				throw new ArgumentNullException(nameof(aggregate));
			}

			var streamName = GetStreamName(aggregate);

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

			await _client.AppendToStreamAsync(streamName, StreamRevision.FromInt64(aggregate.Version), changes, cancellationToken: cancellationToken);

			aggregate.ClearChanges();
		}

		public async Task Save(string streamName, long version, IEnumerable<object> events)
		{
			EventData[] changes = events.Select(@event => new EventData(
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

			await _client.AppendToStreamAsync(streamName, StreamRevision.FromInt64(version), changes);
		}

		public string GetStreamName<T, TId>(TId aggregateId)
		{
			return $"{typeof(T).Name}-{aggregateId!.ToString()}";
		}

		public string GetStreamName<T, TId>(T aggregate) where T : AggregateRoot<TId>
		{
			return $"{typeof(T).Name}-{aggregate.Id!.ToString()}";
		}

		public string GetStreamName<TId>(AggregateRoot<TId> aggregateRoot)
		{
			return $"{aggregateRoot.GetType().Name}-{aggregateRoot.Id!.ToString()}";
		}

		public string GetStreamName(AggregateRootBase aggregateRoot)
		{
			return $"{aggregateRoot.GetType().Name}-{aggregateRoot.GetId()}";
		}

		private async Task<object[]> ReadEventsAsync(EventStoreClient.ReadStreamResult events)
		{
			return await events.Select(resolvedEvent =>
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
			}).ToArrayAsync();
		}

		private byte[] Serialize(object data)
		{
			return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data));
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