using Marketplace.Application.Helpers;

namespace Marketplace.Persistence.EF.UnitOfWork
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
