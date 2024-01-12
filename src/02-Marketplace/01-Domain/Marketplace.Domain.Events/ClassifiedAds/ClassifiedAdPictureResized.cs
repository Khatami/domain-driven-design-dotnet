namespace Marketplace.Domain.Events.ClassifiedAds
{
	public record ClassifiedAdPictureResized(Guid PictureId, int Height, int Width);
}
