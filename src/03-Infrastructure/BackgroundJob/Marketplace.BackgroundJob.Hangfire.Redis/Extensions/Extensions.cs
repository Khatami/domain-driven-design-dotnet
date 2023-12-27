using Hangfire;
using Hangfire.Redis.StackExchange;
using Marketplace.Application.SeedWork.BackgroundJob;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Marketplace.BackgroundJob.Hangfire.Redis.Extensions
{
	public static class Extensions
	{
		public static IServiceCollection AddHangfireServices(this IServiceCollection services, IConfiguration configuration)
		{
			// Other codes / configurations are omitted for brevity.
			var redis = ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnectionString")!);
			services.AddHangfire(configuration =>
			{
				configuration.UseRedisStorage(redis);
			});

			services.AddHangfireServer();

			services.AddScoped<IBackgroundJobService, BackgroundJobService>();

			return services;
		}

		public static void UseHangfireMiddlewares(this IApplicationBuilder app)
		{
			app.UseHangfireDashboard();
		}
	}
}
