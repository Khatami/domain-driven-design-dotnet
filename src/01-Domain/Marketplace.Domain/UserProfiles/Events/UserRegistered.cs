namespace Marketplace.Domain.UserProfiles.Events
{
	public record UserRegistered(Guid UserProfileId, string FullName, string DisplayName);
}
