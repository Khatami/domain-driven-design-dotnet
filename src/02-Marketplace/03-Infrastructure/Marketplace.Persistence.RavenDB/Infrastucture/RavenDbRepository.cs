using Framework.Domain.Aggregation;
using Raven.Client.Documents.Session;

namespace Marketplace.Persistence.RavenDB.Infrastucture
{
	public class RavenDbRepository<T, TId>
		where T : AggregateRoot<TId>
	{
		private readonly IAsyncDocumentSession _session;
		private readonly Func<TId, string> _entityId;

		public RavenDbRepository(IAsyncDocumentSession session, Func<TId, string> entityId)
		{
			_session = session;
			_entityId = entityId;
		}

		public Task AddAsync(T entity)
		{
			return _session.StoreAsync(entity, _entityId(entity.Id));
		}

		public Task<bool> ExistsAsync(TId id)
		{
			return _session.Advanced.ExistsAsync(_entityId(id));
		}

		public Task<T> GetAsync(TId id)
		{
			return _session.LoadAsync<T>(_entityId(id));
		}
	}
}