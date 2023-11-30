using Marketplace.Domain.ClassifiedAds;
using Marketplace.Domain.Shared.ValueObjects;
using Marketplace.Persistence.RavenDB.Infrastucture;
using Raven.Client.Documents.Session;

namespace Marketplace.Persistence.RavenDB.ClassifiedAds
{
	public class ClassifiedAdRepository : RavenDbRepository<ClassifiedAd, ClassifiedAdId>, IClassifiedAdRepository
	{
		public ClassifiedAdRepository(IAsyncDocumentSession session) : base(session, id => $"ClassifiedAd/{id}")
		{
		}
	}
}
