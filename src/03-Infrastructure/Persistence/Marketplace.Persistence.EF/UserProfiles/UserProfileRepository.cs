using Marketplace.Domain.ClassifiedAds;
using Marketplace.Domain.Shared.ValueObjects;
using Marketplace.Domain.UserProfiles;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Persistence.EF.UserProfiles
{
	public class UserProfileRepository : IUserProfileRepository
	{
		private readonly ClassifiedAdDbContext _dbContext;

		public UserProfileRepository(ClassifiedAdDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public Task AddAsync(UserProfile entity)
		{
			return _dbContext.UserProfiles.AddAsync(entity).AsTask();
		}

		public Task<bool> ExistsAsync(UserProfileId id)
		{
			return _dbContext.UserProfiles.AnyAsync(q => q.UserProfileId == id.Value);
		}

		public Task<UserProfile?> GetAsync(UserProfileId id)
		{
			return _dbContext.UserProfiles.FirstOrDefaultAsync(q => q.UserProfileId == id.Value);
		}
	}
}
