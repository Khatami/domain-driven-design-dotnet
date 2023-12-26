using Marketplace.Domain.SeedWork.Streaming;
using Marketplace.Streaming.EventStore.Streaming;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static System.Net.Mime.MediaTypeNames;

namespace Marketplace.Streaming.EventStore.Extensions
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddEventStoreServices(this IServiceCollection services, IConfiguration configuration)
		{
			// The client instance can be used as a singleton across the whole application.
			// It doesn't need to open or close the connection
			services.AddSingleton<IAggregateStore, AggregateStore>();

			return services;
		}
	}
}
