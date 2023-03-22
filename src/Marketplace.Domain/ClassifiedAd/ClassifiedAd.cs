using Marketplace.Domain.ClassifiedAd.Enums;
using Marketplace.Domain.ClassifiedAd.Exceptions;
using Marketplace.Domain.ClassifiedAd.ValueObjects;

namespace Marketplace.Domain.ClassifiedAd
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

		public void SetTitle(string title)
		{
			Title = title;

			EnsureValidState();
		}

		public void UpdateText(string text)
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

					if (Price?.Amount == 0)
						throw new InvalidEntityStateException(this, "amount cannot be zero");
					break;
			}
		}

		public ClassifiedAdId Id { get; }

		public UserId OwnerId { get; }

		public string Title { get; private set; }

		public string Text { get; private set; }

		public Price Price { get; private set; }

		public ClassifiedAdState State { get; private set; }

		public UserId ApprovedBy { get; private set; }
	}
}
