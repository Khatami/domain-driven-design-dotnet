using EventStore.Client;
using Framework.Application.Streaming;
using Framework.Streaming.EventStore.Streaming;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Streaming.EventStore.Extensions
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddEventStoreStreamingServices(this IServiceCollection services, IConfiguration configuration)
		{
			// The client instance can be used as a singleton across the whole application.
			// It doesn't need to open or close the connection
			var connectionString = configuration.GetConnectionString("EventStoreConnectionString");
			if (string.IsNullOrWhiteSpace(connectionString))
			{
				throw new ArgumentNullException(nameof(connectionString));
			}

			var settings = EventStoreClientSettings.Create(connectionString);
			EventStoreClient client = new EventStoreClient(settings);
			services.AddSingleton(client);

			services.AddSingleton<IAggregateStore, AggregateStore>();
			services.AddSingleton<EventStoreProjectionManager>();

			return services;
		}
	}
}
