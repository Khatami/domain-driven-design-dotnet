using Confluent.Kafka.Admin;
using Confluent.Kafka;
using Framework.Application.Streaming;
using Framework.Domain.Aggregation;
using Framework.Streaming.Kafka.Streaming;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Framework.Streaming.Kafka.Extensions
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddKafkaStreamingServices(this IServiceCollection services, IConfiguration configuration)
		{
			List<Type> types = new List<Type>();
			foreach (var assemblyName in Assembly.GetEntryAssembly()!.GetReferencedAssemblies())
			{
				Assembly assembly = Assembly.Load(assemblyName);
				foreach (var type in assembly.GetTypes())
				{
					types.Add(type);
				}
			}

			var aggregations = types
				.Where(current => current.BaseType != null)
				.Where(current => current.BaseType!.IsGenericType)
				.Where(current => current.BaseType!.GetGenericTypeDefinition() == typeof(AggregateRoot<>))
				.Select(current => current.Name)
				.ToList();

			CreateTopicIfNotExists(aggregations, configuration.GetConnectionString("KafkaConnectionString")!).Wait();

			services.AddSingleton<IAggregateStore, AggregateStore>();
			services.AddSingleton<KafkaSubscription>();

			return services;
		}

		private static async Task CreateTopicIfNotExists(List<string> topics, string kafkaConnectionString)
		{
			var adminConfig = new AdminClientConfig
			{
				BootstrapServers = kafkaConnectionString
			};

			using (var adminClient = new AdminClientBuilder(adminConfig).Build())
			{
				foreach (var topicName in topics)
				{
					// Check if topic exists
					var metadata = adminClient.GetMetadata(TimeSpan.FromSeconds(10));
					bool topicExists = metadata.Topics.Exists(t => t.Topic == topicName);

					if (!topicExists)
					{
						// Create topic if it does not exist
						int numPartitions = 1;
						short replicationFactor = 1;
						var topicSpec = new TopicSpecification
						{
							Name = topicName,
							NumPartitions = numPartitions,
							ReplicationFactor = replicationFactor
						};

						await adminClient.CreateTopicsAsync(new TopicSpecification[] { topicSpec });
					}
				}
			}
		}
	}
}
