using Framework.Domain.Aggregation;
using Marketplace.Domain.ClassifiedAds.Entities;
using Marketplace.Domain.ClassifiedAds.Enums;
using Marketplace.Domain.ClassifiedAds.Exceptions;
using Marketplace.Domain.ClassifiedAds.ValueObjects;
using Marketplace.Domain.Events.ClassifiedAds;
using Marketplace.Domain.Events.ClassifiedAds.Snapshot;
using Marketplace.Domain.Shared.ValueObjects;

namespace Marketplace.Domain.ClassifiedAds
{
	public class ClassifiedAd : AggregateRoot<ClassifiedAdId>
	{
		// for impedence mismatch
		private ClassifiedAd()
		{
		}

		public override object GetId()
		{
			return ClassifiedAdId;
		}

		public ClassifiedAd(ClassifiedAdId id, UserProfileId ownerId)
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

		public void Remove()
		{
			Apply(new ClassifiedAdRemoved(Id, DateTime.UtcNow));
		}

		public Guid ClassifiedAdId { get; private set; }

		public UserProfileId OwnerId { get; private set; }

		public ClassifiedAdTitle? Title { get; private set; }

		public ClassifiedAdText? Text { get; private set; }

		public Price? Price { get; private set; }

		public ClassifiedAdState State { get; private set; }

		public UserProfileId? ApprovedBy { get; private set; }

		public bool IsDeleted { get; private set; }

		public DateTimeOffset DeletedOn { get; private set; }

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
					OwnerId = new UserProfileId(e.OwnerId);
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
					ApprovedBy = new UserProfileId(e.ApprovedById);
					break;

				case PictureAddedToAClassifiedAd e:
					var newPicture = new Picture(Apply);
					ApplyToEntity(newPicture, e);
					_pictures.Add(newPicture);
					break;

				case ClassifiedAdRemoved e:
					IsDeleted = true;
					DeletedOn = e.DeletedOn;
					break;

				case ClassifiedAdSnapshotted_V1 e:
					Id = new ClassifiedAdId(e.ClassifiedAdId);
					ClassifiedAdId = e.ClassifiedAdId;
					OwnerId = new UserProfileId(e.OwnerId);

					Title = e.Title != null ? ClassifiedAdTitle.FromString(e.Title) : null;
					Text = e.Text != null ? ClassifiedAdText.FromString(e.Text) : null;
					Price = e.Price != null ? new Price(e.Price.Value, e.CurrencyCode!) : null;
					ApprovedBy = e.ApprovedById != null ? new UserProfileId(e.ApprovedById.Value) : null;

					State = (ClassifiedAdState)e.State;

					_pictures.Clear();
					foreach (var picture in e.Pictures)
					{
						_pictures.Add(Picture.FromSnapshot(picture.PictureId,
							new PictureSize(picture.Width, picture.Height), picture.Url, picture.Order));
					}
					break;

				case ClassifiedAdSnapshotted_V2 e:
					Id = new ClassifiedAdId(e.ClassifiedAdId);
					ClassifiedAdId = e.ClassifiedAdId;
					OwnerId = new UserProfileId(e.OwnerId);

					Title = e.Title != null ? ClassifiedAdTitle.FromString(e.Title) : null;
					Text = e.Text != null ? ClassifiedAdText.FromString(e.Text) : null;
					Price = e.Price != null ? new Price(e.Price.Value, e.CurrencyCode!) : null;
					ApprovedBy = e.ApprovedById != null ? new UserProfileId(e.ApprovedById.Value) : null;

					State = (ClassifiedAdState)e.State;

					IsDeleted = e.IsDeleted;
					DeletedOn = e.DeletedOn;

					_pictures.Clear();
					foreach (var picture in e.Pictures)
					{
						_pictures.Add(Picture.FromSnapshot(picture.PictureId,
							new PictureSize(picture.Width, picture.Height), picture.Url, picture.Order));
					}
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

		public override object GetSnapshotEvent()
		{
			var pictures = Pictures.Select(picture => new ClassifiedAdPictureSnapshot_V2(picture.PictureId,
				picture.Location.ToString(),
				picture.Size.Height, picture.
				Size.Width,
				picture.Order)).ToList();

			return new ClassifiedAdSnapshotted_V2(ClassifiedAdId: ClassifiedAdId,
				OwnerId: OwnerId,
				Title: Title?.Title,
				Text: Text?.Text,
				Price: Price?.Amount,
				CurrencyCode: Price?.Currency.CurrencyCode,
				State: (int)State,
				ApprovedById: ApprovedBy?.Value,
				Pictures: pictures,
				IsDeleted: IsDeleted,
				DeletedOn: DeletedOn);
		}
	}
}
