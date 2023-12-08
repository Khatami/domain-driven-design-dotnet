﻿using Marketplace.Application.Infrastructure.Mediator;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Marketplace.Mediator.Extensions;

public static class ServiceCollectionExtensions
{
	public static void AddMediatorServices
		(this IServiceCollection services, params System.Type[] handlerAssemblyMarkerTypes)
	{
		services.AddTransient<IApplicationMediator, MediatRAdapter>();

		services.AddMediatR(options =>
		{
			options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()!);

			if (handlerAssemblyMarkerTypes.Any())
				options.RegisterServicesFromAssemblies(handlerAssemblyMarkerTypes.Select(current => current.Assembly).ToArray());
		});

		services.AddTransient(typeof(IRequestHandler<,>), typeof(CommandHandlerAdapter<,>));
		services.AddTransient(typeof(IRequestHandler<,>), typeof(CommandUnitHandlerAdapter<>));
		//services.AddTransient(typeof(INotificationHandler<>), typeof(NotificationHandlerAdapter<>));

		services.AddBehaviors();
	}

	public static void AddBehaviors(this IServiceCollection services)
	{
		//services.AddTransient(typeof(Behaviors.IRetriableCommandWithValue<,>), typeof(Behaviors.RetryBehavior<,>));
	}
}
