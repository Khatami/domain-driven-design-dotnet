using Marketplace.Domain.ClassifiedAds.Enums;
using Marketplace.Domain.ClassifiedAds.Exceptions;
using Marketplace.Domain.ClassifiedAds.ValueObjects;

namespace Marketplace.Domain.ClassifiedAds
{
	public class ClassifiedAd
	{
		public ClassifiedAd(ClassifiedAdId id, UserId ownerId)
		{
			Id = id;
			OwnerId = ownerId;
			State = ClassifiedAdState.Inactive;

			EnsureValidState();
		}

		public void SetTitle(ClassifiedAdTitle title)
		{
			Title = title;

			EnsureValidState();
		}

		public void UpdateText(ClassifiedAdText text)
		{
			Text = text;

			EnsureValidState();
		}

		public void UpdatePrice(Price price)
		{
			Price = price;

			EnsureValidState();
		}

		public void RequestToPublish()
		{
			State = ClassifiedAdState.PendingReview;

			EnsureValidState();
		}

		private void EnsureValidState()
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

		public ClassifiedAdId Id { get; }

		public UserId OwnerId { get; }

		public ClassifiedAdTitle Title { get; private set; }

		public ClassifiedAdText Text { get; private set; }

		public Price Price { get; private set; }

		public ClassifiedAdState State { get; private set; }

		public UserId ApprovedBy { get; private set; }
	}
}
