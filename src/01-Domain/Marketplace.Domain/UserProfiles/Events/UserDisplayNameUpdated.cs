namespace Marketplace.Domain.UserProfiles.Events
{
	public record UserDisplayNameUpdated(Guid UserId, string DisplayName);
}
