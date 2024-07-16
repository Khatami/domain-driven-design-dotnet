using System.ComponentModel.DataAnnotations;

namespace Marketplace.ReadModel.PostgreSQL.Models.UserProfiles
{
	public class UserDetail
	{
		[Key]
		public Guid UserProfileId { get; set; }

		public string? DisplayName { get; set; }

        public bool IsDeleted { get; set; }

        [ConcurrencyCheck]
		public long Version { get; set; }
	}
}
