using EventStore.Client;
using Framework.Application.Streaming;
using Framework.Query.Streaming;
using Framework.Streaming.EventStore.Streaming;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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
			services.AddSingleton<EventStoreProjectionManager>();

			var eventStorePersistentSubscriptionsClient = new EventStorePersistentSubscriptionsClient(settings);
			services.AddSingleton(eventStorePersistentSubscriptionsClient);
			services.AddSingleton<EventStorePersistenceSubscription>();

			services.AddSingleton<IAggregateStore, AggregateStore>();

			return services;
		}
	}
}
