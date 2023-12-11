using Marketplace.Domain.Shared.ValueObjects;
using Marketplace.Domain.UserProfiles;

namespace Marketplace.Persistence.EF.UserProfiles
{
	public class UserProfileRepository : IUserProfileRepository
	{
		private readonly ClassifiedAdDbContext _dbContext;

		public UserProfileRepository(ClassifiedAdDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public Task Add(UserProfile entity)
		{
			return _dbContext.UserProfiles.AddAsync(entity).AsTask();
		}

		public async Task<bool> Exists(UserProfileId id)
		{
			return await _dbContext.UserProfiles.FindAsync(id.Value).AsTask() != null;
		}

		public Task<UserProfile?> Load(UserProfileId id)
		{
			return _dbContext.UserProfiles.FindAsync(id.Value).AsTask();
		}
	}
}
