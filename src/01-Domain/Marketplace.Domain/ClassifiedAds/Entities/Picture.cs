using Marketplace.Domain.ClassifiedAds.ValueObjects;
using Marketplace.Domain.Helpers;

namespace Marketplace.Domain.ClassifiedAds.Entities
{
	public class Picture : Entity<PictureId>
	{
		public Picture(PictureSize size, Uri location, int order)
		{
			Size = size;
			Location = location;
			Order = order;
		}

		public PictureSize Size { get; private set; }

		public Uri Location { get; private set; }

		public int Order { get; private set; }

		protected override void When(object @event)
		{ }
	}
}
