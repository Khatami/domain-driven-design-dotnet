﻿using Framework.Domain.Aggregation;
using Marketplace.Domain.ClassifiedAds.ValueObjects;
using Marketplace.Domain.Events.ClassifiedAds;

namespace Marketplace.Domain.ClassifiedAds.Entities
{
	public class Picture : Entity<PictureId>
	{
		private Picture() { }

		public Picture(Action<object> applier) : base(applier)
		{
		}

		// Snapshot usage
	    internal static Picture FromSnapshot(Guid pictureId, PictureSize size, string url, int order)
		{
			return new Picture()
			{
				PictureId = pictureId,
				Size = size,
				Location = new Uri(url),
				Order = order
			};
		}

		public Guid PictureId { get; private set; }

		public PictureSize Size { get; private set; }

		public Uri Location { get; private set; }

		public int Order { get; private set; }

		public void Resize(PictureSize newSize)
		{
			Apply(new ClassifiedAdPictureResized(Id.Value, newSize.Height, newSize.Width));
		}

		protected override void When(object @event)
		{
			switch (@event)
			{
				case PictureAddedToAClassifiedAd e:
					Id = new PictureId(e.PictureId);
					PictureId = e.PictureId;
					Size = new PictureSize(e.Width, e.Height);
					Location = new Uri(e.Url);
					Order = e.Order;
					break;

				case ClassifiedAdPictureResized e:
					Size = new PictureSize(e.Width, e.Height);
					break;
			}
		}
	}
}
