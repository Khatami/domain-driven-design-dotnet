using Marketplace.Application.Shared;
using Raven.Client.Documents.Session;

namespace Marketplace.Persistence.RavenDB.UnitOfWork
{
	public class RavenDBUnitOfWork : IUnitOfWork
	{
		private readonly IAsyncDocumentSession _session;

		public RavenDBUnitOfWork(IAsyncDocumentSession session)
		{
			_session = session;
		}

		public Task Commit()
		{
			return _session.SaveChangesAsync();
		}
	}
}
