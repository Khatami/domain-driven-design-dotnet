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

			builder.OwnsOne(x => x.Id);
			builder.OwnsOne(x => x.Price, p => p.OwnsOne(q => q.Currency));
			builder.OwnsOne(x => x.OwnerId);
			builder.OwnsOne(x => x.Title);
			builder.OwnsOne(x => x.Text);
			builder.OwnsOne(x => x.ApprovedBy);
		}
	}
}
