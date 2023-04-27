namespace Marketplace.Domain.ClassifiedAds.Events
{
	public record ClassifiedAdPictureResized(Guid PictureId, int Height, int Width);
}
