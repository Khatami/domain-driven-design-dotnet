using Marketplace.Domain.ClassifiedAds.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.Persistence.EF.ClassifiedAds.Configurations
{
	internal class PictureConfiguration : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.HasKey(q => q.PictureId);

			builder.OwnsOne(type => type.Id, buildAction =>
			{
				buildAction.Property(property => property.Value).HasColumnName("PK_ImpedanceMismatch");
			});

			builder.OwnsOne(q => q.Size);
		}
    }
}
