namespace Marketplace.Domain.Events.UserProfiles.Snapshot
{
	public record UserProfileSnapshotted_V1(Guid UserProfileId, string FullName, string DisplayName, string? PhotoUrl);
}
