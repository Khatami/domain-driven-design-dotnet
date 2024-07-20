using Confluent.Kafka;
using Framework.Query.Attributes;
using Framework.Query.Streaming;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Framework.Streaming.Kafka.Streaming
{
	public class KafkaSubscription
	{
		private readonly string _kafkaConnectionString;
		private readonly ConsumerConfig _config;
		private readonly string _groupName;
		private readonly IProjection[] _projections;

		public KafkaSubscription(IConfiguration configuration, IProjection[] projections)
		{
			_kafkaConnectionString = configuration.GetConnectionString("KafkaConnectionString")!;
			_groupName = configuration.GetSection("ServiceSettings:ServiceName").Get<string>()!;

			_config = new ConsumerConfig
			{
				BootstrapServers = _kafkaConnectionString,
				GroupId = _groupName,
				EnableAutoCommit = true,
				AutoOffsetReset = AutoOffsetReset.Latest // Specify offset behavior when there is no initial offset in Kafka or if the current offset does not exist any more on the server (e.g., because that data has been deleted).
			};

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

			using (var consumer = new ConsumerBuilder<Ignore, string>(_config).Build())			
			{
				consumer.Subscribe(streams);

				//while(true)
				//{
					//var consumeResult = consumer.Consume();

					//consumer.Commit(consumeResult);
				//}
			}
		}
	}
}
