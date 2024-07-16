using Framework.Application.BackgroundJob;
using Framework.Application.Streaming;
using Framework.Application.UnitOfWork;
using Framework.Domain.Aggregation;
using Framework.Domain.Comparison;
using Framework.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using System.Transactions;

namespace Framework.Persistence.EFCore
{
	public class EfCoreUnitOfWorkBase<T> : IUnitOfWork where T : DbContext
	{
		private readonly T _dbContext;
		private readonly IAggregateStore _aggregateStore;
		private readonly IBackgroundJobService _backgroundJobService;
		private readonly IComparisonService _comparisonService;
		private readonly IServiceProvider _serviceProvider;

		public EfCoreUnitOfWorkBase(T dbContext,
			IAggregateStore aggregateStore,
			IBackgroundJobService backgroundJobService,
			IComparisonService comparisonService,
			IServiceProvider serviceProvider)
		{
			_dbContext = dbContext;
			_aggregateStore = aggregateStore;
			_backgroundJobService = backgroundJobService;
			_comparisonService = comparisonService;
			_serviceProvider = serviceProvider;
		}

		public virtual async Task Commit(CancellationToken cancellationToken)
		{
			var entries = _dbContext.ChangeTracker.Entries<AggregateRootBase>()
				.Where(current => current.Entity.GetType().IsSubclassOf(typeof(AggregateRootBase)))
				.ToList();

			using TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

			try
			{
				foreach (EntityEntry<AggregateRootBase>? entry in entries)
				{
					if (entry.State != EntityState.Deleted)
					{
						var originalEntity = await GetOriginalEntityAsync(entry);

						var checkEventStreaming = entry.Entity.CheckEventStreaming(originalEntity, _comparisonService);

						if (checkEventStreaming == false)
						{
							throw new ApplicationException($"The latest state of the entity [{entry.Entity.GetType()}, Id={entry.Entity.GetId()}] does not match the existing events");
						}
					}
					else
					{
						if (entry.Entity.GetChanges()
							.Any(current => current.GetType() == typeof(AggregationRemoved)) == false)
						{
							throw new ApplicationException($"For removing an aggregation, it is required to call Remove().");
						}
					}

					var version = entry.Entity.GetLatestVersion();

					string streamName = _aggregateStore.GetStreamName(entry.Entity);
					var changes = entry.Entity.GetChanges();

					_backgroundJobService.Enqueue(BackgroundJobConsts.Outbox,
						() => _aggregateStore.Save(streamName, entry.Entity.Version, changes));

					entry.Entity.ClearChanges();

					entry.Property(current => current.Version).CurrentValue = version;
				}

				await _dbContext.SaveChangesAsync(cancellationToken);

				transaction.Complete();
			}
			catch (Exception)
			{
				transaction.Dispose();

				throw;
			}
		}

		private async Task<AggregateRootBase> GetOriginalEntityAsync(EntityEntry<AggregateRootBase> entry)
		{
			using var serviceScope = _serviceProvider.CreateScope();

			var dbContext = serviceScope.ServiceProvider.GetRequiredService<T>();

			object? originalEntity = await dbContext
				.FindAsync(entry.Entity.GetType(), entry.Entity.GetId());

			var result = (AggregateRootBase?)originalEntity;

			if (result != null)
			{
				return result;
			}

			return (AggregateRootBase)Activator.CreateInstance(entry.Entity.GetType(), true)!;
		}
	}
}