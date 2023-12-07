using Raven.Client.Documents;
using Marketplace.Infrastructure;
using Raven.Client.Documents.Session;
using Marketplace.Mediator.Extensions;
using Marketplace.Domain.UserProfiles.Delegates;
using Marketplace.Domain.ClassifiedAds.DomainServices;

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

			services.AddBehaviors();
			services.AddMediatorServices();

			return services;
		}
	}
}
