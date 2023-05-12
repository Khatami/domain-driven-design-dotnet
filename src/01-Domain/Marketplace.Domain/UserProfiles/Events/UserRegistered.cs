namespace Marketplace.Domain.UserProfiles.Events
{
	public record UserRegistered(Guid UserId, string FullName, string DisplayName);
}
