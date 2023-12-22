using Marketplace.Domain.SeedWork.Streaming;
using Marketplace.Persistence.EventStore.Streaming;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Marketplace.Persistence.EventStore.Extensions
{
    public static class ServiceExtensions
	{
		public static IServiceCollection AddEventStoreServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<IAggregateStore, AggregateStore>();

			return services;
		}
	}
}
