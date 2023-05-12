using Marketplace.Domain;
using Marketplace.Domain.ClassifiedAds;
using Marketplace.Domain.Shared.ValueObjects;
using Raven.Client.Documents.Session;

namespace Marketplace.Persistence.RavenDB.ClassifiedAds
{
    public class ClassifiedAdRepository : IClassifiedAdRepository
	{
		private readonly IAsyncDocumentSession _session;

		public ClassifiedAdRepository(IAsyncDocumentSession session)
		{
			_session = session;
		}

		private static string EntityId (ClassifiedAdId classifiedAdId)
		{
			return $"ClassifiedAd/{classifiedAdId}";
		}

		public Task Add(ClassifiedAd entity)
		{
			return _session.StoreAsync(entity, EntityId(entity.Id));
		}

		public Task<bool> Exists(ClassifiedAdId id)
		{
			return _session.Advanced.ExistsAsync(EntityId(id));
		}

		public Task<ClassifiedAd> Load(ClassifiedAdId id)
		{
			return _session.LoadAsync<ClassifiedAd>(EntityId(id));
		}
	}
}
