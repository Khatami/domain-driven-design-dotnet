using Marketplace.Application.ClassifiedAds.CommandHandlers;
using Marketplace.Application.ClassifiedAds.Services;
using Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;
using Marketplace.Application.Contracts.ClassifiedAds.IServices;
using Marketplace.Application.Contracts.UserProfiles.IServices;
using Marketplace.Application.Infrastructure;
using Marketplace.Application.UserProfiles.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Marketplace.Application.Extensions
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			// Composition pattern
			services.AddScoped<IHandleCommand<CreateClassifiedAd>>
				(q => new RetryingCommandHandler<CreateClassifiedAd>(new CreateClassifiedAdCommandHandler()));

			services.AddScoped<IClassifiedAdApplicationService, ClassifiedAdApplicationService>();
			services.AddScoped<IUserProfileApplicationService, UserProfileApplicationService>();

			return services;
		}
	}
}
