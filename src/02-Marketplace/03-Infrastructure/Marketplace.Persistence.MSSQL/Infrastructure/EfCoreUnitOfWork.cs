using Framework.Application.BackgroundJob;
using Framework.Application.Streaming;
using Framework.Domain.Comparison;
using Framework.Persistence.EFCore;

namespace Marketplace.Persistence.MSSQL.Infrastructure
{
	public class EfCoreUnitOfWork : EfCoreUnitOfWorkBase<MarketplaceDbContext>
	{
		public EfCoreUnitOfWork(MarketplaceDbContext dbContext,
			IAggregateStore aggregateStore,
			IBackgroundJobService backgroundJobService,
			IComparisonService comparisonService,
			IServiceProvider serviceProvider) : base (dbContext, aggregateStore, backgroundJobService, comparisonService, serviceProvider)
		{
		}
	}
}