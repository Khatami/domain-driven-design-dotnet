using Marketplace.Application.Infrastructure.Mediator;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace Marketplace.Mediator.Extensions;

public static class ServiceCollectionExtensions
{
	public static void AddMediatorServices
		(this IServiceCollection services, params System.Type[] handlerAssemblyMarkerTypes)
	{
		services.AddTransient<IMediator, CustomMediator>();

		services.AddMediatR(options =>
		{
			options.RegisterServicesFromAssembly(Assembly.GetEntryAssembly()!);

			if (handlerAssemblyMarkerTypes.Any())
				options.RegisterServicesFromAssemblies(handlerAssemblyMarkerTypes.Select(current => current.Assembly).ToArray());
		});

		services.AddBehaviors();
	}

	public static void AddBehaviors(this Microsoft.Extensions.DependencyInjection.IServiceCollection services)
	{
		services.AddTransient(typeof(Behaviors.IRetriableCommandWithValue<,>), typeof(Behaviors.RetryBehavior<,>));
	}
}
