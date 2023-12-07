using Microsoft.Extensions.DependencyInjection;
using Marketplace.Application.UserProfiles.Services;
using Marketplace.Application.Contracts.UserProfiles.IServices;

namespace Marketplace.Application.Extensions;

public static class ServiceExtensions
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		services.AddTransient<IUpdateUserProfileService, UpdateUserProfileService>();

		return services;
	}
}
