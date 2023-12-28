using Hangfire;
using Marketplace.Application.SeedWork.BackgroundJob;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Marketplace.BackgroundJob.Hangfire.MSSQL.Extensions
{
	public static class Extensions
	{
		public static IServiceCollection AddMSSQLHangfireServices(this IServiceCollection services,
			IConfiguration configuration,
			string connectionString,
			int defaultQueueWorkCount = 50,
			int outboxQueueWorkCount = 50,
			int inboxQueueWorkCount = 50)
		{
			if (string.IsNullOrWhiteSpace(connectionString))
			{
				throw new ArgumentNullException(nameof(connectionString));
			}

			services.AddHangfire(configuration =>
			{
				configuration.UseSqlServerStorage(connectionString);
			});

			services.AddHangfireServer(q =>
			{
				q.WorkerCount = defaultQueueWorkCount;
			});

			services.AddHangfireServer(q =>
			{
				q.Queues = new string[] { BackgroundJobConsts.Outbox };
				q.WorkerCount = outboxQueueWorkCount;
			});

			services.AddHangfireServer(q =>
			{
				q.Queues = new string[] { BackgroundJobConsts.Inbox };
				q.WorkerCount = inboxQueueWorkCount;
			});

			services.AddScoped<IBackgroundJobService, BackgroundJobService>();

			return services;
		}

		public static void UseHangfireMiddlewares(this IApplicationBuilder app)
		{
			app.UseHangfireDashboard();
		}
	}
}
