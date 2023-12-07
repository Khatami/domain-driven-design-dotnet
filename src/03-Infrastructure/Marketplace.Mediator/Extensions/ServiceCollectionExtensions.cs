﻿using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Marketplace.Mediator.Extensions;

public static class ServiceCollectionExtensions
{
	public static void AddMediatorServices
		(this Microsoft.Extensions.DependencyInjection.IServiceCollection services, params System.Type[] handlerAssemblyMarkerTypes)
	{
		services.AddMediatR
			(configuration => configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

		services.AddTransient<ICustomMediator, CustomMediator>();

		services.AddMediatR(options =>
		{
			options.RegisterServicesFromAssemblies(handlerAssemblyMarkerTypes.Select(current => current.Assembly).ToArray());
		});

		services.AddBehaviors();
	}

	public static void AddBehaviors(this Microsoft.Extensions.DependencyInjection.IServiceCollection services)
	{
		services.AddTransient(typeof(Behaviors.IRetriableCommandWithValue<,>), typeof(Behaviors.RetryBehavior<,>));
	}
}
