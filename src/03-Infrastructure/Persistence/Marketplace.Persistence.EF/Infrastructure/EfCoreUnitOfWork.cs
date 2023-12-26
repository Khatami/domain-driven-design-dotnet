using Marketplace.Application.SeedWork.UnitOfWork;

namespace Marketplace.Persistence.EF.Infrastructure
{
	public class EfCoreUnitOfWork : IUnitOfWork
	{
		private readonly ClassifiedAdDbContext _dbContext;

		public EfCoreUnitOfWork(ClassifiedAdDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public Task Commit()
		{
			return _dbContext.SaveChangesAsync();
		}
	}
}
