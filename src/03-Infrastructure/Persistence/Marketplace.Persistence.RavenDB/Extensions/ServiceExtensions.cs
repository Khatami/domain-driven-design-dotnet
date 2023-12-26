using Marketplace.Application.SeedWork.UnitOfWork;
using Marketplace.Domain.UserProfiles;
using Marketplace.Persistence.RavenDB.Infrastucture;
using Marketplace.Persistence.RavenDB.UserProfiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Marketplace.Persistence.RavenDB.Extensions
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddRavenDBServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<IUnitOfWork, RavenDBUnitOfWork>();
			services.AddScoped<IUserProfileRepository, UserProfileRepository>();

			return services;
		}
	}
}
