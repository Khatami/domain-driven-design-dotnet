using Marketplace.Application.ClassifiedAds.Commands;
using Marketplace.Application.Contracts.ClassifiedAds.V1;
using Marketplace.Application.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Marketplace.Application.Extensions
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddSingleton<IHandleCommand<ClassifiedAd_Create_V1>>
				(q => new RetryingCommandHandler<ClassifiedAd_Create_V1>(new CreateClassifiedAdCommandHandler()));

			return services;
		}
	}
}
