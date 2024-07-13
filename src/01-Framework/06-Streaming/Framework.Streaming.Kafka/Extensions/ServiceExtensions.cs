using Framework.Application.Streaming;
using Framework.Streaming.Kafka.Streaming;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Streaming.Kafka.Extensions
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddKafkaStreamingServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton<IAggregateStore, AggregateStore>();

			return services;
		}
	}
}
