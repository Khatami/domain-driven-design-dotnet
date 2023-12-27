using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Marketplace.Application.SeedWork.BackgroundJob
{
	public interface IBackgroundJobService
	{
		string Enqueue<T>([NotNull] Expression<Action<T>> methodCall);

		string Enqueue<T>([NotNull] string queue, [NotNull] Expression<Action<T>> methodCall);

		string Enqueue([NotNull] Expression<Func<Task>> methodCall);

		string Enqueue([NotNull] string queue, [System.Diagnostics.CodeAnalysis.NotNull] Expression<Func<Task>> methodCall);
	}
}
