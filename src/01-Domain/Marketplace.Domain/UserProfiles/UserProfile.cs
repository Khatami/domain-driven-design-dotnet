using Marketplace.Domain.Shared.Helpers;
using Marketplace.Domain.Shared.ValueObjects;
using Marketplace.Domain.UserProfiles.Events;
using Marketplace.Domain.UserProfiles.ValueObjects;

namespace Marketplace.Domain.UserProfiles
{
	public class UserProfile : AggregateRoot<UserId>
	{
		// for impedence mismatch
		private UserProfile() { }

		public UserProfile(
			UserId id,
			FullName fullName,
			DisplayName displayName)
		{
			Apply(new UserRegistered(id, fullName, displayName));
		}

		public void UpdateFullName(FullName fullName)
		{
			Apply(new UserFullNameUpdated(Id, fullName));
		}

		public void UpdateDisplayName(FullName fullName)
		{
			Apply(new UserFullNameUpdated(Id, fullName));
		}

		public void UpdateProfilePhoto(Uri photoUri)
		{
			Apply(new ProfilePhotoUpdated(Id, photoUri.ToString()));
		}

		public Guid UserId { get; private set; }

		public FullName FullName { get; private set; }

		public DisplayName DisplayName { get; private set; }

		public string PhotoUrl { get; private set; }

		protected override void When(object @event)
		{
			switch(@event)
			{
				case UserRegistered e:
					Id = new UserId(e.UserId);
					UserId = e.UserId;
					FullName = FullName.FromString(e.FullName);
					DisplayName = DisplayName.FromString(e.DisplayName, null);
					break;

				case UserFullNameUpdated e:
					FullName = FullName.FromString(e.FullName);
					break;

				case UserDisplayNameUpdated e:
					DisplayName = DisplayName.FromString(e.DisplayName, null);
					break;

				case ProfilePhotoUpdated e:
					PhotoUrl = e.PhotoUrl;
					break;
			}
		}

		// To Check Entity Invariant
		protected override void EnsureValidState()
		{
		}
	}
}
