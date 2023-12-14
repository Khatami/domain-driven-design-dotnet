﻿using Marketplace.Domain.ClassifiedAds.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.Persistence.EF.ClassifiedAds.Configurations
{
	internal class PictureConfiguration : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.HasKey(q => q.PictureId);

            builder.Ignore(q => q.Id);

			builder.OwnsOne(q => q.Size);
		}
    }
}
