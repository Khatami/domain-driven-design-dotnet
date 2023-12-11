using Marketplace.Domain.UserProfiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.Persistence.EF.UserProfiles.Configurations
{
	internal class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
	{
		public void Configure(EntityTypeBuilder<UserProfile> builder)
		{
			builder.HasKey(x => x.UserId);

			builder.OwnsOne(x => x.Id);
			builder.OwnsOne(x => x.FullName);
			builder.OwnsOne(x => x.DisplayName);
		}
	}
}
