using Marketplace.Domain.UserProfiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.Persistence.EF.UserProfiles.Configurations
{
	internal class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
	{
		public void Configure(EntityTypeBuilder<UserProfile> builder)
		{
			builder.HasKey(x => x.UserProfileId);

			builder.Property(a => a.Version).IsConcurrencyToken();

			builder.OwnsOne(type => type.Id, buildAction =>
			{
				buildAction.Property(property => property.Value).HasColumnName("PK_ImpedanceMismatch");
			});

			builder.OwnsOne(type => type.FullName, buildAction =>
			{
				buildAction.Property(property => property.Value).HasColumnName(nameof(UserProfile.FullName));
			});

			builder.OwnsOne(type => type.DisplayName, buildAction =>
			{
				buildAction.Property(property => property.Value).HasColumnName(nameof(UserProfile.DisplayName));
			});
		}
	}
}
