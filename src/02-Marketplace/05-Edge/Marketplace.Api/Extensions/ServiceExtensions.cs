using Autofac;
using Framework.Application.Mediator;
using Framework.Mediator.MediatR.Extensions;
using Framework.Query.Mediator;
using Marketplace.Api.Infrastructure;
using Marketplace.Domain.ClassifiedAds.DomainServices;
using Marketplace.Domain.UserProfiles.Delegates;
using Raven.Client.Documents;
using System.Reflection;

namespace Marketplace.Api.Extensions
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddEdgeServices(this IServiceCollection services, IConfiguration configuration)
		{
			var store = new DocumentStore
			{
				Urls = new[] { configuration.GetSection("RavenDB").GetValue<string>("Url") },
				Database = configuration.GetSection("RavenDB").GetValue<string>("Database"),
				Conventions =
				{
					FindIdentityProperty = m => m.Name == "_databaseId"
				}
			};

			store.Initialize();
			services.AddScoped(c => store.OpenAsyncSession());

			services.AddScoped<ICurrencyLookup, FixedCurrencyLookup>();

			var purgomalumClient = new PurgomalumClient();
			services.AddSingleton<CheckTextForProfanity>(text =>
			{
				return purgomalumClient.CheckForProfanity(text).Result;
			});

			services.AddMediatorServices();

			return services;
		}

		public static void AddAutofacServices(this ConfigureHostBuilder builder)
		{
			builder.ConfigureContainer<ContainerBuilder>(builder =>
			{
				var openTypes = new[]
				{
					typeof(ICommandHandler<,>),
					typeof(ICommandHandler<>),
					typeof(IQueryHandler<,>)
				};

				List<Assembly> assemblies = new List<Assembly>()
				{
					typeof(Application.AppInfo).Assembly
				};

				assemblies.Add(typeof(Queries.AppInfo).Assembly);

				foreach (var openType in openTypes)
				{
					builder
						.RegisterAssemblyTypes(assemblies.ToArray())
						.AsClosedTypesOf(openType)
						.AsImplementedInterfaces()
						.InstancePerLifetimeScope();
				}
			});
		}
	}
}
