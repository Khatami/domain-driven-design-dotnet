using Marketplace.Domain.Base;
using Marketplace.Domain.ClassifiedAds.Enums;
using Marketplace.Domain.ClassifiedAds.Events;
using Marketplace.Domain.ClassifiedAds.Exceptions;
using Marketplace.Domain.ClassifiedAds.ValueObjects;

namespace Marketplace.Domain.ClassifiedAds
{
	public class ClassifiedAd : Entity
	{
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

		public void RequestToPublish()
		{
			Apply(new ClassifiedAdSentForReview(Id));
		}

		protected override void When(object @event)
		{
			// advanced pattern matching
			switch (@event)
			{
				case ClassifiedAdCreated e:
					Id = new ClassifiedAdId(e.Id);
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
					break;
			}
		}

		protected override void EnsureValidState()
		{
			if (Id == null)
				throw new InvalidEntityStateException(this, "title cannot be empty");

			if (OwnerId == null)
				throw new InvalidEntityStateException(this, "text cannot be empty");

			switch (State)
			{
				case ClassifiedAdState.PendingReview:
				case ClassifiedAdState.Active:
					if (Title == null)
						throw new InvalidEntityStateException(this, "title cannot be empty");

					if (Text == null)
						throw new InvalidEntityStateException(this, "text cannot be empty");

					if (Price == null || Price.Amount == 0)
						throw new InvalidEntityStateException(this, "amount cannot be zero");
					break;
			}
		}

		public ClassifiedAdId Id { get; private set; }

		public UserId OwnerId { get; private set; }

		public ClassifiedAdTitle Title { get; private set; }

		public ClassifiedAdText Text { get; private set; }

		public Price Price { get; private set; }

		public ClassifiedAdState State { get; private set; }

		public UserId ApprovedBy { get; private set; }
	}
}
