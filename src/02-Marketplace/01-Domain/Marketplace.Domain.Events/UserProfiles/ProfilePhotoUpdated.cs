namespace Marketplace.Domain.Events.UserProfiles
{
	public record ProfilePhotoUpdated(Guid UserId, string PhotoUrl);
}
