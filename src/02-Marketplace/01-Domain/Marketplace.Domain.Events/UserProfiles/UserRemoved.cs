namespace Marketplace.Domain.Events.UserProfiles
{
	public record UserRemoved(Guid UserId, DateTimeOffset DeletedOn);
}
