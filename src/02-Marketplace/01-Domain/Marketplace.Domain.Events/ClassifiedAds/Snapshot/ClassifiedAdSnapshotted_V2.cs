namespace Marketplace.Domain.Events.ClassifiedAds.Snapshot
{
	public record ClassifiedAdSnapshotted_V2
	(
		Guid ClassifiedAdId,
		Guid OwnerId,
		string? Title,
		string? Text,
		decimal? Price,
		string? CurrencyCode,
		int State,
		Guid? ApprovedById,
		List<ClassifiedAdPictureSnapshot_V2> Pictures,
		bool IsDeleted,
		DateTimeOffset DeletedOn
	);

	public record ClassifiedAdPictureSnapshot_V2(Guid PictureId, string Url, int Height, int Width, int Order);
}