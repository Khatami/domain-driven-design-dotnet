using Marketplace.Application.Helpers;
using Marketplace.Persistence.EF.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace Marketplace.Persistence.EF.Extensions
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddEFServices(this IServiceCollection services)
		{
			services.AddScoped<IUnitOfWork, EfCoreUnitOfWork>();

			return services;
		}
	}
}
