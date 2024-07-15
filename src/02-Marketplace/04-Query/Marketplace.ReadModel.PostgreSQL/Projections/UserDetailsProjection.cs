using Framework.Query.Attributes;
using Framework.Query.Streaming;
using Marketplace.Domain.Events.UserProfiles;
using Marketplace.ReadModel.PostgreSQL.Exceptions;
using Marketplace.ReadModel.PostgreSQL.Models.UserProfiles;

namespace Marketplace.ReadModel.PostgreSQL.Projections
{
	[Streaming(Stream.UserProfile)]
	public class UserDetailsProjection : IProjection
	{
		private readonly MarketplaceReadModelDbContext _databaseContext;

		public UserDetailsProjection(MarketplaceReadModelDbContext databaseContext)
		{
			_databaseContext = databaseContext;
		}

		public async Task Project(object @event, string stream, long eventNumberInStream, long version)
		{
			switch (@event)
			{
				case UserRegistered e:
					var exists = _databaseContext.UserDetails.Any(x => x.UserProfileId == e.UserProfileId);

					if (exists == false)
					{
						_databaseContext.UserDetails.Add(new UserDetail
						{
							UserProfileId = e.UserProfileId,
							DisplayName = e.DisplayName,
							Version = version
						});
					}
					break;
				case UserDisplayNameUpdated e:
					UpdateItem(e.UserId, x =>
					{
						x.DisplayName = e.DisplayName;
					}, version);
					break;

				default:
					break;
			}

			await _databaseContext.SaveChangesAsync();
		}

		private void UpdateItem(Guid id, Action<UserDetail> update, long version)
		{
			var item = _databaseContext.UserDetails.FirstOrDefault(x => x.UserProfileId == id);

			if (item == null)
			{
				return;
			}

			if (version <= item.Version)
			{
				return;
			}

			if (item.Version + 1 != version)
			{
				throw new ConcurrencyMismatchException();
			}

			item.Version = version;

			update(item);
		}
	}
}
