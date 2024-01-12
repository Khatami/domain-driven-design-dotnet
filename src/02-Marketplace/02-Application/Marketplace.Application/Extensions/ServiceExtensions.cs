using Microsoft.Extensions.DependencyInjection;

namespace Marketplace.Application.Extensions;

public static class ServiceExtensions
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		return services;
	}
}
