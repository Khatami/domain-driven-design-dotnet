using Marketplace.Domain.Shared.ValueObjects;
using Marketplace.Domain.UserProfiles;
using Marketplace.Persistence.RavenDB.Infrastucture;
using Raven.Client.Documents.Session;

namespace Marketplace.Persistence.RavenDB.UserProfiles
{
	public class UserProfileRepository : RavenDbRepository<UserProfile, UserId>, IUserProfileRepository
	{
		public UserProfileRepository(IAsyncDocumentSession session) : base(session, id => $"UserProfile/{id}")
		{
		}
	}
}
