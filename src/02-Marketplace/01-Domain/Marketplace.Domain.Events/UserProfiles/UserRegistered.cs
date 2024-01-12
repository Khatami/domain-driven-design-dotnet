namespace Marketplace.Domain.Events.UserProfiles
{
	public record UserRegistered(Guid UserProfileId, string FullName, string DisplayName);
}
