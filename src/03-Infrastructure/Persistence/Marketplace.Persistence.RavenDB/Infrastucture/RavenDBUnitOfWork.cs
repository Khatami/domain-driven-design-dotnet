using Marketplace.Application.SeedWork.UnitOfWork;
using Raven.Client.Documents.Session;

namespace Marketplace.Persistence.RavenDB.Infrastucture
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
