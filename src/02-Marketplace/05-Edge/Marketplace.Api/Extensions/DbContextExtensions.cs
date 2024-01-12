using Marketplace.Persistence.MSSQL;

namespace Marketplace.Api.Extensions
{
	public static class DbContextExtensions
	{
		public static void EnsureDatabaseCreated(this IApplicationBuilder app)
		{
			using (var scope = app.ApplicationServices.CreateScope())
			{
				var context = scope.ServiceProvider.GetRequiredService<ClassifiedAdDbContext>();

				context.Database.EnsureCreated();
			}
		}
	}
}
