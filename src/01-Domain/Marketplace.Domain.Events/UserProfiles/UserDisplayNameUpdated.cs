namespace Marketplace.Domain.Events.UserProfiles
{
	public record UserDisplayNameUpdated(Guid UserId, string DisplayName);
}
