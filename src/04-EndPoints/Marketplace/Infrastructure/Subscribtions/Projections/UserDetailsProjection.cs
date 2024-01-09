using Marketplace.Domain.Events.UserProfiles;
using Marketplace.Infrastructure.Subscribtions.Infrastructure;
using Marketplace.Queries.Contracts.ReadModels.UserProfiles;

namespace Marketplace.Infrastructure.Subscribtions.Projections
{
	public class UserDetailsProjection : IProjection
	{
		public static List<UserDetail> UserDetails = new();

		public Task Project(object @event)
		{
			switch (@event)
			{
				case UserRegistered e:
					UserDetails.Add(new UserDetail
					{
						UserProfileId = e.UserProfileId,
						DisplayName = e.DisplayName
					});
					break;
				case UserDisplayNameUpdated e:
					UpdateItem(e.UserId, x => x.DisplayName = e.DisplayName);
					break;
			}

			return Task.CompletedTask;
		}

		private void UpdateItem(Guid id, Action<UserDetail> update)
		{
			var item = UserDetails.FirstOrDefault(x => x.UserProfileId == id);

			if (item == null)
			{
				return;
			}

			update(item);
		}
	}
}
