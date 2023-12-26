using Marketplace.Application.SeedWork.UnitOfWork;
using Marketplace.Domain.SeedWork.Aggregation;
using Marketplace.Domain.SeedWork.Streaming;

namespace Marketplace.Persistence.EF.Infrastructure
{
	public class EfCoreUnitOfWork : IUnitOfWork
	{
		private readonly ClassifiedAdDbContext _dbContext;
		private readonly IAggregateStore _aggregateStore;

		public EfCoreUnitOfWork(ClassifiedAdDbContext dbContext, IAggregateStore aggregateStore)
		{
			_dbContext = dbContext;
			_aggregateStore = aggregateStore;
		}

		public async Task Commit(CancellationToken cancellationToken)
		{
			var entries =
				_dbContext.ChangeTracker.Entries<AggregateRootBase>()
				.Where(current => current.Entity.GetType().IsSubclassOf(typeof(AggregateRootBase)))
				.ToList();

			// TODO: Outbox Pattern is required
			foreach (var entry in entries)
			{
				var version = entry.Entity.GetLatestVersion();
				await _aggregateStore.Save(entry.Entity, cancellationToken);

				entry.Property(q => q.Version).CurrentValue = version;
			}

			await _dbContext.SaveChangesAsync(cancellationToken);
		}
	}
}
