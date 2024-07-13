using Autofac;
using Framework.Application.Mediator;
using Framework.BackgroundJob.Hangfire.MSSQL.Extensions;
using Framework.Mediator.MediatR.Extensions;
using Framework.Query.Mediator;
using Framework.Query.Streaming;
using Framework.Streaming.EventStore.Extensions;
using Framework.Streaming.EventStore.Streaming;
using Framework.Streaming.Kafka.Extensions;
using Marketplace.Api.Infrastructure;
using Marketplace.Domain.ClassifiedAds.DomainServices;
using Marketplace.Domain.UserProfiles.Delegates;
using Marketplace.Persistence.MSSQL;
using Marketplace.Persistence.MSSQL.Extensions;
using Marketplace.Persistence.RavenDB.Extensions;
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

		public static void AddCQRSServices(this ConfigureHostBuilder builder)
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

		public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
		{
			var persistenceApproach = configuration
				.GetSection("ServiceSettings")
				.GetValue<PersistenceApproach>("PersistenceApproach");

			switch (persistenceApproach)
			{
				case PersistenceApproach.RavenDB:
					services.AddRavenDBServices(configuration);
					break;

				case PersistenceApproach.EntityFramework:
					string? connectionString = configuration.GetConnectionString("SqlServerConnectionString");

					if (string.IsNullOrWhiteSpace(connectionString))
					{
						throw new ArgumentNullException(nameof(connectionString));
					}

					services.AddEFServices(configuration, connectionString);
					services.AddMSSQLHangfireServices(configuration, connectionString);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(persistenceApproach));
			}
		}

		public static void AddStreamingServices(this IServiceCollection services,
			IConfiguration configuration,
			ConfigureHostBuilder builder)
		{
			var streamingApproach = configuration
				.GetSection("ServiceSettings")
				.GetValue<StreamingApproach>("StreamingApproach");

			switch (streamingApproach)
			{
				case StreamingApproach.EventStore:
					services.AddEventStoreStreamingServices(configuration);

					builder.ConfigureContainer<ContainerBuilder>(builder =>
					{
						var types = new[]
						{
							typeof(IProjection),
						};

						List<Assembly> assemblies = new List<Assembly>();

						assemblies.Add(typeof(ReadModel.PostgreSQL.AppInfo).Assembly);

						foreach (var type in types)
						{
							builder
								.RegisterAssemblyTypes(assemblies.ToArray())
								.Where(current => current.IsAssignableTo(type))
								.AsImplementedInterfaces()
								.InstancePerLifetimeScope();
						}
					});
					break;
				case StreamingApproach.Kafka:
					services.AddKafkaStreamingServices(configuration);
					break;
			}
		}

		public static void EnsureDatabaseCreated(this IApplicationBuilder app, IConfiguration configuration)
		{
			var persistenceApproach = configuration
				.GetSection("ServiceSettings")
				.GetValue<PersistenceApproach>("PersistenceApproach");

			if (persistenceApproach == PersistenceApproach.EntityFramework)
			{
				var marketplaceDbContext = app.ApplicationServices.GetRequiredService<MarketplaceDbContext>();
				marketplaceDbContext.Database.EnsureCreated();
			}
		}

		public static void StartProjections(this IApplicationBuilder app, IConfiguration configuration)
		{
			var streamingApproach = configuration
				.GetSection("ServiceSettings")
				.GetValue<StreamingApproach>("StreamingApproach");

			if (streamingApproach == StreamingApproach.EventStore)
			{
				app.ApplicationServices.GetRequiredService<EventStoreProjectionManager>().Start();
			}
		}

		public static bool IsUAT(this IHostEnvironment hostEnvironment)
		{
			return hostEnvironment.IsEnvironment("UAT");
		}
	}
}
