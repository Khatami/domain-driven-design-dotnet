using Marketplace.Domain.ClassifiedAds;
using Marketplace.Domain.ClassifiedAds.ValueObjects;
using Marketplace.Persistence.EF;

namespace Marketplace.Persistence.EF.ClassifiedAds
{
	public class ClassifiedAdRepository : IClassifiedAdRepository
	{
		private readonly ClassifiedAdDbContext _dbContext;

		public ClassifiedAdRepository(ClassifiedAdDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public Task Add(ClassifiedAd entity)
		{
			return _dbContext.ClassifiedAds.AddAsync(entity);
		}

		public async Task<bool> Exists(ClassifiedAdId id)
		{
			await _dbContext.ClassifiedAds.FindAsync(id.Value) != null;
		}

		public Task<ClassifiedAd> Load(ClassifiedAdId id)
		{
			return _dbContext.ClassifiedAds.FindAsync(id.Value);
		}
	}
}
