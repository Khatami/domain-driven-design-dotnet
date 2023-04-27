namespace Marketplace.Domain.ClassifiedAds.Events
{
	public record PictureAddedToAClassifiedAd(Guid PictureId, Guid ClassifiedAdId, string Url, int Height, int Width, int Order);
}
