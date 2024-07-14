using Marketplace.ReadModel.PostgreSQL.Models.ClassifiedAds;
using Marketplace.ReadModel.PostgreSQL.Models.UserProfiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Marketplace.ReadModel.PostgreSQL
{
	public class MarketplaceReadModelDbContext : DbContext
	{
		public MarketplaceReadModelDbContext(DbContextOptions<MarketplaceReadModelDbContext> options)
			: base(options)
		{
		}

		public DbSet<ClassifiedAdDetail> ClassifiedAdDetails { get; set; }

		public DbSet<ClassifiedAdItem> ClassifiedAdItems { get; set; }

		public DbSet<UserDetail> UserDetails { get; set; }
	}
}
