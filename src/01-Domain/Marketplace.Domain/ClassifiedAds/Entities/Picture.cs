﻿using Marketplace.Domain.ClassifiedAds.Events;
using Marketplace.Domain.ClassifiedAds.ValueObjects;

namespace Marketplace.Domain.ClassifiedAds.Entities
{
	public class Picture : Entity<PictureId>
	{
		public Picture(Action<object> applier) : base(applier)
		{
		}

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