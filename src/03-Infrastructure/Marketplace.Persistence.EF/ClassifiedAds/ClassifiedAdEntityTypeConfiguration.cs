using Marketplace.Domain.ClassifiedAds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data;

namespace Marketplace.Persistence.EF.ClassifiedAds
{
	internal class ClassifiedAdEntityTypeConfiguration : IEntityTypeConfiguration<ClassifiedAd>
	{
		public void Configure(EntityTypeBuilder<ClassifiedAd> builder)
		{
			builder
				.OwnsOne(x => x.Id, q =>
				{
					q.Property(p => p.Value)
						.HasColumnType(SqlDbType.UniqueIdentifier.ToString())
						.IsRequired()
						.HasColumnName(nameof(ClassifiedAd.Id));
				})
				.HasKey(x => x.Id);
		}
	}
}
