using Confluent.Kafka;
using Framework.Application.BackgroundJob;
using Framework.Query.Attributes;
using Framework.Query.Streaming;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Framework.Streaming.Kafka.Streaming
{
	public class KafkaSubscription
	{
		private readonly string _kafkaConnectionString;
		private readonly string _groupName;

		private readonly ConsumerConfig _config;
		private readonly IProjection[] _projections;
		private readonly IBackgroundJobService _backgroundJobService;

		public KafkaSubscription(IConfiguration configuration, IProjection[] projections, IBackgroundJobService backgroundJobService)
		{
			_kafkaConnectionString = configuration.GetConnectionString("KafkaConnectionString")!;
			_groupName = configuration.GetSection("ServiceSettings:ServiceName").Get<string>()!;

			_config = new ConsumerConfig
			{
				BootstrapServers = _kafkaConnectionString,
				GroupId = _groupName,
				EnableAutoCommit = false
			};

			_projections = projections;
			_backgroundJobService = backgroundJobService;
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

			Thread thread = new Thread(ConsumerThread!);

			var parameters = new ThreadParameter(streams, _config, _projections);
			thread.Start(parameters);
		}

		private void ConsumerThread(object parameters)
		{
			var consumerParameters = (ThreadParameter)parameters;

			using (var consumer = new ConsumerBuilder<Ignore, string>(consumerParameters.Config).Build())
			{
				consumer.Subscribe(consumerParameters.Streams);

				while (true)
				{
					ConsumeResult<Ignore, string> consumeResult = consumer.Consume(TimeSpan.FromSeconds(1));

					if (consumeResult != null)
					{
						try
						{
							var projections = consumerParameters.Projections
								.Where(current => current.GetType().GetCustomAttribute<StreamingAttribute>(true)!.Streams.Contains(consumeResult.Topic))
								.ToList();

							var eventTypeHeader = consumeResult.Message.Headers.FirstOrDefault(current => current.Key == "eventType");
							var versionHeader = consumeResult.Message.Headers.FirstOrDefault(current => current.Key == "version");

							if (eventTypeHeader != null && versionHeader != null)
							{
								var eventTypeValue = Encoding.UTF8.GetString(eventTypeHeader.GetValueBytes());
								var versionValue = Encoding.UTF8.GetString(versionHeader.GetValueBytes());
								var version = long.Parse(versionValue);

								var dataType = Type.GetType(eventTypeValue);

								if (dataType != null)
								{
									var data = JsonSerializer.Deserialize(consumeResult.Message.Value, dataType);

									if (data != null)
									{
										foreach (var projection in projections)
										{
											_backgroundJobService.Enqueue(BackgroundJobConsts.Inbox,
												() => projection.Project(data, consumeResult.Topic, consumeResult.Offset, version));
										}

										consumer.Commit(consumeResult);
									}
								}
							}
						}
						catch (Exception ex)
						{
							//TODO: Log
						}
					}
				}
			}
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

		private class ThreadParameter
		{
			public ThreadParameter(List<string> streams, ConsumerConfig config, IProjection[] projections)
			{
				Streams = streams;
				Config = config;
				Projections = projections;
			}

			public List<string> Streams { get; set; }
			public ConsumerConfig Config { get; set; }
			public IProjection[] Projections { get; set; }
		}
	}
}
