using Framework.Application.UnitOfWork;
using Marketplace.Domain.UserProfiles;
using Marketplace.Persistence.MSSQL.Infrastructure;
using Marketplace.Persistence.MSSQL.UserProfiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Marketplace.Persistence.MSSQL.Extensions
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddEFServices(this IServiceCollection services,
			IConfiguration configuration,
			string? connectionString)
		{
			if (string.IsNullOrWhiteSpace(connectionString))
			{
				throw new ArgumentNullException(nameof(connectionString));
			}

			services.AddScoped<IUnitOfWork, EfCoreUnitOfWork>();
			services.AddScoped<IUserProfileRepository, UserProfileRepository>();

			services.AddDbContext<MarketplaceDbContext>(options =>
			{
				options.UseSqlServer(connectionString);
			});

			return services;
		}
	}
}
