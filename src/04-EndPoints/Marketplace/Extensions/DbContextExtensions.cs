using Marketplace.Persistence.EF;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Extensions
{
	public static class DbContextExtensions
	{
		public static void EnsureDatabase(this IApplicationBuilder app)
		{
			var context = app.ApplicationServices.GetRequiredService<ClassifiedAdDbContext>();

			if (context.Database.EnsureCreated() == false)
			{
				context.Database.Migrate();
			}
		}
	}
}
