namespace Marketplace.Domain.Events.ClassifiedAds
{
	public record PictureAddedToAClassifiedAd(Guid PictureId, Guid ClassifiedAdId, string Url, int Height, int Width, int Order);
}
