using Marketplace.Domain.ClassifiedAds;
using Marketplace.Domain.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Persistence.EF.ClassifiedAds
{
	public class ClassifiedAdRepository : IClassifiedAdRepository
	{
		private readonly ClassifiedAdDbContext _dbContext;

		public ClassifiedAdRepository(ClassifiedAdDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public Task AddAsync(ClassifiedAd entity)
		{
			return _dbContext.ClassifiedAds.AddAsync(entity).AsTask();
		}

		public Task<bool> ExistsAsync(ClassifiedAdId id)
		{
			return _dbContext.ClassifiedAds.AnyAsync(q => q.ClassifiedAdId == id.Value);
		}

		public Task<ClassifiedAd?> GetAsync(ClassifiedAdId id)
		{
			return _dbContext.ClassifiedAds.FirstOrDefaultAsync(q => q.ClassifiedAdId == id.Value);
		}
	}
}
