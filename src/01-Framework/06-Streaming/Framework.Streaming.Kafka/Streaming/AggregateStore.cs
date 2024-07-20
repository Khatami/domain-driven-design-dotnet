using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Framework.Application.Streaming;
using Framework.Domain.Aggregation;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace Framework.Streaming.Kafka.Streaming
{
	internal class AggregateStore : IAggregateStore
	{
		private readonly string _kafkaConnectionString;

		public AggregateStore(IConfiguration configuration)
        {
			_kafkaConnectionString = configuration.GetConnectionString("KafkaConnectionString")!;
        }

        public async Task<bool> Exists<T, TId>(TId aggregateId)
		{
			throw new NotImplementedException("EventSourcing has not been Implemented For Kafka yet");
		}

		public Task<T> Load<T, TId>(TId aggregateId) where T : AggregateRoot<TId>
		{
			throw new NotImplementedException("EventSourcing has not been Implemented For Kafka yet");
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

		public async Task Save<T, TId>(T aggregate, CancellationToken cancellationToken) where T : AggregateRoot<TId>
		{
			await Save($"{aggregate.GetType().Name}-{aggregate.Id}", aggregate.Version, aggregate.GetChanges());
		}

		public async Task Save<TId>(AggregateRoot<TId> aggregate, CancellationToken cancellationToken = default)
		{
			await Save($"{aggregate.GetType().Name}-{aggregate.Id}", aggregate.Version, aggregate.GetChanges());
		}

		public async Task Save(AggregateRootBase aggregate, CancellationToken cancellationToken = default)
		{
			await Save($"{aggregate.GetType().Name}-{aggregate.GetId()}", aggregate.Version, aggregate.GetChanges());
		}

		public async Task Save(string streamName, long version, IEnumerable<object> events)
		{
			var config = new ProducerConfig
			{
				BootstrapServers = _kafkaConnectionString
			};

			var topic = streamName.Split("-").First();
			var id = streamName.Replace($"{streamName.Split("-").First()!}-", string.Empty);

			using (var producer = new ProducerBuilder<string, string>(config).Build())
			{
				int index = 1;
				foreach (var @event in events)
				{
					await producer.ProduceAsync(topic, new Message<string, string>
					{
						Key = id,
						Value = JsonSerializer.Serialize(@event),
						Headers = new Headers
						{
							{ "eventType", Encoding.UTF8.GetBytes(@event.GetType().AssemblyQualifiedName!) },
							{ "version", Encoding.UTF8.GetBytes((version + index).ToString()) }
						}
					});

					index += 1;
				}

				producer.Flush();
			}
		}
	}
}