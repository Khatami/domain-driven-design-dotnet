using Framework.Query.Streaming;
using Marketplace.Domain.Events.UserProfiles;
using Marketplace.ReadModel.PostgreSQL.Models.UserProfiles;

namespace Marketplace.ReadModel.PostgreSQL.Projections
{
	public class UserDetailsProjection : IProjection
	{
		internal static List<UserDetail> UserDetails = new();

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
