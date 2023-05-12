namespace Marketplace.Domain.UserProfiles.Events
{
	public record ProfilePhotoUpdated(Guid UserId, string PhotoUrl);
}
