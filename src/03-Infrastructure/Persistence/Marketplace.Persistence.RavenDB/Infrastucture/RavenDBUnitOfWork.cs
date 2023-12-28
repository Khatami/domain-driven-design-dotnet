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

        public Task Commit(CancellationToken cancellationToken)
        {
            /* 
             * TODO: If you choose RavenDB or and other database types for persisting data you should 
             * implement outbox pattern here
             * so raven db persistence is not ready for event streaming yet
            */

            // TODO: versioning

            return _session.SaveChangesAsync(cancellationToken);
        }
    }
}
