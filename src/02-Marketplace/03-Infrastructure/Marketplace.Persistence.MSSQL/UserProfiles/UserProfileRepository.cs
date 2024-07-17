using Marketplace.Domain.ClassifiedAds;
using Marketplace.Domain.Shared.ValueObjects;
using Marketplace.Domain.UserProfiles;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Persistence.MSSQL.UserProfiles
{
	public class UserProfileRepository : IUserProfileRepository
	{
		private readonly MarketplaceDbContext _dbContext;

		public UserProfileRepository(MarketplaceDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public Task AddAsync(UserProfile entity)
		{
			return _dbContext.UserProfiles.AddAsync(entity).AsTask();
		}

		public Task<UserProfile?> GetAsync(UserProfileId id)
		{
			return _dbContext.UserProfiles
				.FirstOrDefaultAsync(q => q.UserProfileId == id.Value);
		}
	}
}
