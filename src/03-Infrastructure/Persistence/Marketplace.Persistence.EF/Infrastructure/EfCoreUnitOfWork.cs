﻿using Marketplace.Application.SeedWork.BackgroundJob;
using Marketplace.Application.SeedWork.UnitOfWork;
using Marketplace.Domain.SeedWork.Aggregation;
using Marketplace.Domain.SeedWork.Streaming;
using System.Transactions;

namespace Marketplace.Persistence.EF.Infrastructure
{
	public class EfCoreUnitOfWork : IUnitOfWork
	{
		private readonly ClassifiedAdDbContext _dbContext;
		private readonly IAggregateStore _aggregateStore;
		private readonly IBackgroundJobService _backgroundJobService;

		public EfCoreUnitOfWork(ClassifiedAdDbContext dbContext, IAggregateStore aggregateStore,
			IBackgroundJobService backgroundJobService)
		{
			_dbContext = dbContext;
			_aggregateStore = aggregateStore;
			_backgroundJobService = backgroundJobService;
		}

		public async Task Commit(CancellationToken cancellationToken)
		{
			var entries = _dbContext.ChangeTracker.Entries<AggregateRootBase>()
				.Where(current => current.Entity.GetType().IsSubclassOf(typeof(AggregateRootBase)))
				.ToList();

			using TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

			try
			{
				foreach (var entry in entries)
				{
					var version = entry.Entity.GetLatestVersion();

					_backgroundJobService.Enqueue(BackgroundJobConsts.Outbox,
						() => _aggregateStore.Save(_aggregateStore.GetStreamName(entry.Entity), entry.Entity.Version, entry.Entity.GetChanges()));

					entry.Entity.ClearChanges();

					entry.Property(q => q.Version).CurrentValue = version;
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
	}
}
