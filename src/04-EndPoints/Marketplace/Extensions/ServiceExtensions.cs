using Marketplace.Domain.ClassifiedAds.DomainServices;
using Marketplace.Infrastructure;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

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

			// TODO: Profanity Resolve

			return services;
		}
	}
}
