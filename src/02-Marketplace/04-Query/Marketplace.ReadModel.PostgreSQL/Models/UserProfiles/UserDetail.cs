using System.ComponentModel.DataAnnotations;

namespace Marketplace.ReadModel.PostgreSQL.Models.UserProfiles
{
	public class UserDetail
	{
		[Key]
		public Guid UserProfileId { get; set; }
		public string DisplayName { get; set; }
	}
}
