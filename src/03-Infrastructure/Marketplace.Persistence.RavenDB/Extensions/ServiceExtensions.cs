﻿using Marketplace.Application.Helpers;
using Marketplace.Persistence.RavenDB.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using static Raven.Client.Constants;

namespace Marketplace.Persistence.RavenDB.Extensions
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddRavenDBServices(this IServiceCollection services)
		{
			services.AddScoped<IUnitOfWork, RavenDBUnitOfWork>();

			return services;
		}
	}
}
