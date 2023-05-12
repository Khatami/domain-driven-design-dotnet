using Marketplace.Domain.ClassifiedAds.ValueObjects;

namespace Marketplace.Domain.UserProfiles
{
	public interface IUserProfileRepository
	{
		Task Add(UserProfile entity);

		Task<bool> Exists(UserProfile id);

		Task<UserProfile?> Load(UserProfile id);
	}
}