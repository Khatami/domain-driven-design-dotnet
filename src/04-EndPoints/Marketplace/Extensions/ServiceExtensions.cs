using Autofac;
using Marketplace.Application.SeedWork.Mediator;
using Marketplace.Domain.ClassifiedAds.DomainServices;
using Marketplace.Domain.UserProfiles.Delegates;
using Marketplace.Infrastructure;
using Marketplace.Mediator.MediatR.Extensions;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System.Reflection;

namespace Marketplace.Extensions
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
			services.AddScoped<IAsyncDocumentSession>(c => store.OpenAsyncSession());

			services.AddScoped<ICurrencyLookup, FixedCurrencyLookup>();

			var purgomalumClient = new PurgomalumClient();
			services.AddSingleton<CheckTextForProfanity>(text =>
			{
				return purgomalumClient.CheckForProfanity(text).Result;
			});

			services.AddMediatorServices();

			return services;
		}

		public static void AddAutofacServices(this ConfigureHostBuilder builder, PersistenceApproach persistenceApproach)
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
					typeof(Application.Extensions.ServiceExtensions).Assembly
				};

				switch (persistenceApproach)
				{
					case PersistenceApproach.RavenDB:
						assemblies.Add(typeof(Marketplace.Queries.RavenDB.AppInfo).Assembly);
						break;
					case PersistenceApproach.EntityFramework:
						assemblies.Add(typeof(Marketplace.Queries.EF.AppInfo).Assembly);
						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(persistenceApproach));
				}

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
