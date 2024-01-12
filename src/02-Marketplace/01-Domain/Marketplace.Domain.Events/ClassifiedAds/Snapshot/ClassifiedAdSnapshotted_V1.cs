namespace Marketplace.Domain.Events.ClassifiedAds.Snapshot
{
	public record ClassifiedAdSnapshotted_V1
	(
		Guid ClassifiedAdId,
		Guid OwnerId,
		string? Title,
		string? Text,
		decimal? Price,
		string? CurrencyCode,
		int State,
		Guid? ApprovedById,
		List<ClassifiedAdPictureSnapshot_V1> Pictures
	);

	public record ClassifiedAdPictureSnapshot_V1(Guid PictureId, string Url, int Height, int Width, int Order);
}