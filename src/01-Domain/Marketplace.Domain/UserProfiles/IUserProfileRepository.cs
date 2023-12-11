using Marketplace.Domain.Shared.ValueObjects;

namespace Marketplace.Domain.UserProfiles
{
	public interface IUserProfileRepository
	{
		Task<UserProfile> Load(UserProfileId id);

		Task Add(UserProfile entity);

		Task<bool> Exists(UserProfileId id);
	}
}