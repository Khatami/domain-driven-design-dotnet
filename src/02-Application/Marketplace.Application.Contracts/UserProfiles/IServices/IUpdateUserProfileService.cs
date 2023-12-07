using Marketplace.Domain.UserProfiles;

namespace Marketplace.Application.Contracts.UserProfiles.IServices;

public interface IUpdateUserProfileService
{
	Task HandleUpdate(Guid userProfileId, Action<UserProfile> operation);
}
