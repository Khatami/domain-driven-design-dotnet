using Framework.Application.BackgroundJob;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Framework.BackgroundJob.Hangfire.MSSQL
{
	public class BackgroundJobService : IBackgroundJobService
	{
		public string Enqueue<T>([NotNull] Expression<Action<T>> methodCall)
		{
			return global::Hangfire.BackgroundJob.Enqueue(methodCall);
		}

		public string Enqueue<T>([NotNull] string queue, [NotNull] Expression<Action<T>> methodCall)
		{
			return global::Hangfire.BackgroundJob.Enqueue(queue, methodCall);
		}

		public string Enqueue([NotNull] Expression<Func<Task>> methodCall)
		{
			return global::Hangfire.BackgroundJob.Enqueue(methodCall);
		}

		public string Enqueue([NotNull] string queue, [NotNull] Expression<Func<Task>> methodCall)
		{
			return global::Hangfire.BackgroundJob.Enqueue(queue, methodCall);
		}
	}
}
