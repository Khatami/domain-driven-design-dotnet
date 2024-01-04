using Marketplace.Domain.Events.UserProfiles;
using Marketplace.Domain.Events.UserProfiles.Snapshot;
using Marketplace.Domain.SeedWork.Aggregation;
using Marketplace.Domain.Shared.ValueObjects;
using Marketplace.Domain.UserProfiles.ValueObjects;

namespace Marketplace.Domain.UserProfiles
{
	public class UserProfile : AggregateRoot<UserProfileId>
	{
		// for impedence mismatch
		private UserProfile() { }

		public UserProfile(
			UserProfileId id,
			FullName fullName,
			DisplayName displayName)
		{
			Apply(new UserRegistered(id, fullName, displayName));
		}

		public void UpdateFullName(FullName fullName)
		{
			Apply(new UserFullNameUpdated(Id, fullName));
		}

		public void UpdateDisplayName(DisplayName displayName)
		{
			Apply(new UserDisplayNameUpdated(Id, displayName));
		}

		public void UpdateProfilePhoto(Uri photoUri)
		{
			Apply(new ProfilePhotoUpdated(Id, photoUri.ToString()));
		}

		public Guid UserProfileId { get; private set; }

		public FullName FullName { get; private set; }

		public DisplayName DisplayName { get; private set; }

		public string? PhotoUrl { get; private set; }

		protected override void When(object @event)
		{
			switch (@event)
			{
				case UserRegistered e:
					Id = new UserProfileId(e.UserProfileId);
					UserProfileId = e.UserProfileId;
					FullName = new FullName(e.FullName);
					DisplayName = new DisplayName(e.DisplayName);
					break;

				case UserFullNameUpdated e:
					FullName = new FullName(e.FullName);
					break;

				case UserDisplayNameUpdated e:
					DisplayName = new DisplayName(e.DisplayName);
					break;

				case ProfilePhotoUpdated e:
					PhotoUrl = e.PhotoUrl;
					break;

				case UserProfileSnapshotted_V1 e:
					Id = new UserProfileId(e.UserProfileId);
					UserProfileId = e.UserProfileId;
					FullName = new FullName(e.FullName);
					DisplayName = new DisplayName(e.DisplayName);

					PhotoUrl = e.PhotoUrl;
					break;
			}
		}

		// To Check Entity Invariant
		protected override void EnsureValidState()
		{
		}

		public override object GetSnapshotEvent()
		{
			return new UserProfileSnapshotted_V1(UserProfileId, FullName, DisplayName, PhotoUrl);
		}
	}
}
