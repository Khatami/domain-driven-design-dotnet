using Marketplace.Application.Shared;
using Marketplace.Domain.ClassifiedAds;
using Marketplace.Domain.UserProfiles;
using Marketplace.Persistence.RavenDB.ClassifiedAds;
using Marketplace.Persistence.RavenDB.Infrastucture;
using Marketplace.Persistence.RavenDB.UserProfiles;
using Microsoft.Extensions.DependencyInjection;

namespace Marketplace.Persistence.RavenDB.Extensions
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddRavenDBServices(this IServiceCollection services)
		{
			services.AddScoped<IUnitOfWork, RavenDBUnitOfWork>();
			services.AddScoped<IClassifiedAdRepository, ClassifiedAdRepository>();
			services.AddScoped<IUserProfileRepository, UserProfileRepository>();

			return services;
		}
	}
}
