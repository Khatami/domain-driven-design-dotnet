using Marketplace.Application.Shared;
using Marketplace.Domain.ClassifiedAds;
using Marketplace.Persistence.RavenDB.ClassifiedAds;
using Marketplace.Persistence.RavenDB.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace Marketplace.Persistence.RavenDB.Extensions
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddRavenDBServices(this IServiceCollection services)
		{
			services.AddScoped<IUnitOfWork, RavenDBUnitOfWork>();
			services.AddScoped<IClassifiedAdRepository, ClassifiedAdRepository>();

			return services;
		}
	}
}
