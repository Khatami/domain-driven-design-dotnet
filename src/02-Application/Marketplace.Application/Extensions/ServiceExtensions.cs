using Marketplace.Application.ClassifiedAds.CommandHandlers;
using Marketplace.Application.ClassifiedAds.Services;
using Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;
using Marketplace.Application.Contracts.ClassifiedAds.IServices;
using Marketplace.Application.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Marketplace.Application.Extensions
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped<IHandleCommand<ClassifiedAd_Create_V1>>
				(q => new RetryingCommandHandler<ClassifiedAd_Create_V1>(new CreateClassifiedAdCommandHandler()));

			services.AddScoped<IClassifiedAdService, ClassifiedAdService>();

			return services;
		}
	}
}
