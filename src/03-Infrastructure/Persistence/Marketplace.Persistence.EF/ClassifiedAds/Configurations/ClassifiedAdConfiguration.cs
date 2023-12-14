using Marketplace.Domain.ClassifiedAds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.Persistence.EF.ClassifiedAds.Configurations
{
	internal class ClassifiedAdConfiguration : IEntityTypeConfiguration<ClassifiedAd>
	{
		public void Configure(EntityTypeBuilder<ClassifiedAd> builder)
		{
			builder.HasKey(x => x.ClassifiedAdId);

			builder.OwnsOne(type => type.Id, buildAction =>
			{
				buildAction.Property(property => property.Value).HasColumnName("PK_ImpedanceMismatch");
			});

			builder.OwnsOne(x => x.Price, p => p.OwnsOne(q => q.Currency));

			builder.OwnsOne(type => type.OwnerId, buildAction =>
			{
				buildAction.Property(property => property.Value).HasColumnName(nameof(ClassifiedAd.OwnerId));
			});

			builder.OwnsOne(type => type.Title, buildAction =>
			{
				buildAction.Property(property => property.Title).HasColumnName(nameof(ClassifiedAd.Title));
			});

			builder.OwnsOne(type => type.Text, buildAction =>
			{
				buildAction.Property(property => property.Text).HasColumnName(nameof(ClassifiedAd.Text));
			});

			builder.OwnsOne(type => type.ApprovedBy, buildAction =>
			{
				buildAction.Property(property => property.Value).HasColumnName(nameof(ClassifiedAd.ApprovedBy));
			});
		}
	}
}
