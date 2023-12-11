using Marketplace.Domain.ClassifiedAds;
using Marketplace.Domain.UserProfiles;
using Marketplace.Persistence.EF.ClassifiedAds;
using Marketplace.Persistence.EF.ClassifiedAds.Configurations;
using Marketplace.Persistence.EF.UserProfiles.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Marketplace.Persistence.EF
{
	public class ClassifiedAdDbContext : DbContext
	{
		private readonly ILoggerFactory _loggerFactory;

		public ClassifiedAdDbContext(DbContextOptions<ClassifiedAdDbContext> options, ILoggerFactory loggerFactory)
			: base(options)
		{
			_loggerFactory = loggerFactory;
		}

		public DbSet<ClassifiedAd> ClassifiedAds { get; set; }

		public DbSet<UserProfile> UserProfiles { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseLoggerFactory(_loggerFactory);
			optionsBuilder.EnableSensitiveDataLogging();

			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// ClassifiedAds
			modelBuilder.ApplyConfiguration(new ClassifiedAdConfiguration());
			modelBuilder.ApplyConfiguration(new PictureConfiguration());

			// UserProfiles
			modelBuilder.ApplyConfiguration(new UserProfileConfiguration());

			base.OnModelCreating(modelBuilder);
		}
	}
}
