using Marketplace.Domain.ClassifiedAds.Entities;
using Marketplace.Domain.ClassifiedAds.Enums;
using Marketplace.Domain.ClassifiedAds.Events;
using Marketplace.Domain.ClassifiedAds.Exceptions;
using Marketplace.Domain.ClassifiedAds.ValueObjects;
using Marketplace.Domain.Infrastructure;
using Marketplace.Domain.Shared.ValueObjects;

namespace Marketplace.Domain.ClassifiedAds
{
	public class ClassifiedAd : AggregateRoot<ClassifiedAdId>
	{
		// for impedence mismatch
		private ClassifiedAd() { }

		public ClassifiedAd(ClassifiedAdId id, UserId ownerId)
		{
			Apply(new ClassifiedAdCreated(id, ownerId));
		}

		public void SetTitle(ClassifiedAdTitle title)
		{
			Apply(new ClassifiedAdTitleChanged(Id, title));
		}

		public void UpdateText(ClassifiedAdText text)
		{
			Apply(new ClassifiedAdTextChanged(Id, text));
		}

		public void UpdatePrice(Price price)
		{
			Apply(new ClassifiedAdPriceUpdated(Id, price.Amount, price.Currency.CurrencyCode));
		}

		public void RequestToPublish(Guid approvedById)
		{
			Apply(new ClassifiedAdSentForReview(Id, approvedById));
		}

		public void AddPicture(Uri pictureUri, PictureSize pictureSize)
		{
			int order = 1;

			if (Pictures.Any())
				order = Pictures?.Max(x => x.Order) ?? 0 + 1;

			Apply(new PictureAddedToAClassifiedAd
			(
				Guid.NewGuid(),
				Id, 
				pictureUri.ToString(), 
				pictureSize.Height, 
				pictureSize.Width, 
				order
			));
		}

		public Guid ClassifiedAdId { get; private set; }

		public UserId OwnerId { get; private set; }

		public ClassifiedAdTitle? Title { get; private set; }

		public ClassifiedAdText? Text { get; private set; }

		public Price? Price { get; private set; }

		public ClassifiedAdState State { get; private set; }

		public UserId? ApprovedBy { get; private set; }

		private List<Picture> _pictures = new();
		public IReadOnlyList<Picture> Pictures
		{
			get
			{
				return _pictures;
			}
		}

		private Picture? FindPicture(PictureId id)
		{
			return Pictures.FirstOrDefault(q => q.Id == id);
		}

		public void ResizePicture(PictureId pictureId, PictureSize newSize)
		{
			var picture = FindPicture(pictureId);

			if (picture == null)
				throw new InvalidOperationException("Cannot resize a picture that I don't have");

			picture.Resize(newSize);
		}

		private Picture? FirstPicture => Pictures.OrderBy(q => q.Order).FirstOrDefault();

		protected override void When(object @event)
		{
			// advanced pattern matching
			switch (@event)
			{
				case ClassifiedAdCreated e:
					Id = new ClassifiedAdId(e.Id);
					ClassifiedAdId = e.Id;
					OwnerId = new UserId(e.OwnerId);
					State = ClassifiedAdState.Inactive;
					break;

				case ClassifiedAdTitleChanged e:
					Title = ClassifiedAdTitle.FromString(e.Title);
					break;

				case ClassifiedAdTextChanged e:
					Text = ClassifiedAdText.FromString(e.AdText);
					break;

				case ClassifiedAdPriceUpdated e:
					// Because Price is already validated by using LookupService, we can trust it
					Price = new Price(e.Price, e.CurrencyCode);
					break;

				case ClassifiedAdSentForReview e:
					State = ClassifiedAdState.PendingReview;
					ApprovedBy = new UserId(e.ApprovedById);
					break;

				case PictureAddedToAClassifiedAd e:
					var newPicture = new Picture(Apply);
					ApplyToEntity(newPicture, e);
					_pictures.Add(newPicture);
					break;
			}
		}

		// To Check Entity Invariant
		protected override void EnsureValidState()
		{
			if (Id == null)
				throw new InvalidEntityStateException(this, "title cannot be empty");

			if (OwnerId == null)
				throw new InvalidEntityStateException(this, "text cannot be empty");

			switch (State)
			{
				case ClassifiedAdState.PendingReview:
					if (Title == null)
						throw new InvalidEntityStateException(this, "title cannot be empty");

					if (Text == null)
						throw new InvalidEntityStateException(this, "text cannot be empty");

					if (Price == null || Price.Amount == 0)
						throw new InvalidEntityStateException(this, "amount cannot be zero");

					if (ApprovedBy == null)
						throw new InvalidEntityStateException(this, "ApprovedBy cannot be null");

					if (FirstPicture == null)
						throw new InvalidEntityStateException(this, "FirstPicture cannot be null");

					if (FirstPicture.Size.Width < 600 || FirstPicture.Size.Height < 800)
						throw new InvalidPictureSizeException("First Picture should be larger than 600X800 pixel");
					break;

				case ClassifiedAdState.Active:
					if (Title == null)
						throw new InvalidEntityStateException(this, "title cannot be empty");

					if (Text == null)
						throw new InvalidEntityStateException(this, "text cannot be empty");

					if (Price == null || Price.Amount == 0)
						throw new InvalidEntityStateException(this, "amount cannot be zero");

					if (FirstPicture == null)
						throw new InvalidEntityStateException(this, "FirstPicture cannot be null");
					break;
			}
		}
	}
}
