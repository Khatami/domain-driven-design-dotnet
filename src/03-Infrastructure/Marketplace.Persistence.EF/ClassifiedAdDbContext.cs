using Marketplace.Domain.ClassifiedAds;
using Marketplace.Persistence.EF.ClassifiedAds;
using Marketplace.Persistence.EF.ClassifiedAds.Configurations;
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

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseLoggerFactory(_loggerFactory);
			optionsBuilder.EnableSensitiveDataLogging();

			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new ClassifiedAdEntityTypeConfiguration());
			modelBuilder.ApplyConfiguration(new PictureEntityTypeConfiguration());

			base.OnModelCreating(modelBuilder);
		}
	}
}
