using Marketplace.Persistence.EF;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Extensions
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
