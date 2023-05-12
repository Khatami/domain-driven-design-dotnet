namespace Marketplace.Domain.UserProfiles.Events
{
	public record UserFullNameUpdated(Guid UserId, string FullName);
}
