using Marketplace.Domain.UserProfiles;
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

		public DbSet<UserProfile> UserProfiles { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseLoggerFactory(_loggerFactory);
			optionsBuilder.EnableSensitiveDataLogging();

			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// UserProfiles
			modelBuilder.ApplyConfiguration(new UserProfileConfiguration());

			base.OnModelCreating(modelBuilder);
		}
	}
}
