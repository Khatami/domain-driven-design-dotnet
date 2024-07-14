using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Marketplace.ReadModel.PostgreSQL.Extensions
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddReadModelServices(this IServiceCollection services,
			IConfiguration configuration)
		{
			string? connectionString = configuration.GetConnectionString("PostgresConnectionString");

			if (string.IsNullOrWhiteSpace(connectionString))
			{
				throw new ArgumentNullException(nameof(connectionString));
			}

			services.AddDbContext<MarketplaceReadModelDbContext>(options =>
			{
				options.UseNpgsql(connectionString);
			});

			return services;
		}
	}
}
