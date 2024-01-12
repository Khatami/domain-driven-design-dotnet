using Marketplace.Domain.Shared.ValueObjects;

namespace Marketplace.Domain.UserProfiles
{
	public interface IUserProfileRepository
	{
		Task<UserProfile> GetAsync(UserProfileId id);

		Task AddAsync(UserProfile entity);

		Task<bool> ExistsAsync(UserProfileId id);
	}
}