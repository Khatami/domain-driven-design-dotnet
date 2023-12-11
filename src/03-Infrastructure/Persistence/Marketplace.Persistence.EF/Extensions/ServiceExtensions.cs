using Marketplace.Application.Infrastructure.UnitOfWork;
using Marketplace.Domain.ClassifiedAds;
using Marketplace.Domain.UserProfiles;
using Marketplace.Persistence.EF.ClassifiedAds;
using Marketplace.Persistence.EF.Infrastructure;
using Marketplace.Persistence.EF.UserProfiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Marketplace.Persistence.EF.Extensions
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddEFServices(this IServiceCollection services, IConfiguration configuration)
		{
			string? connectionString = configuration.GetConnectionString("SqlServerConnectionString");

			services.AddScoped<IUnitOfWork, EfCoreUnitOfWork>();
			services.AddScoped<IClassifiedAdRepository, ClassifiedAdRepository>();
			services.AddScoped<IUserProfileRepository, UserProfileRepository>();

			services.AddDbContext<ClassifiedAdDbContext>(options => options.UseSqlServer(connectionString));

			return services;
		}
	}
}
