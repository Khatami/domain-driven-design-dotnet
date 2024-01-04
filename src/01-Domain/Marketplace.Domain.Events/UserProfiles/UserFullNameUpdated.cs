namespace Marketplace.Domain.Events.UserProfiles
{
	public record UserFullNameUpdated(Guid UserId, string FullName);
}
