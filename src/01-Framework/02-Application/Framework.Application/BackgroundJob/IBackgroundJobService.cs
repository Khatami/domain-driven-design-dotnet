using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Framework.Application.BackgroundJob
{
	public interface IBackgroundJobService
	{
		string Enqueue<T>([NotNull] Expression<Action<T>> methodCall);

		string Enqueue<T>([NotNull] string queue, [NotNull] Expression<Action<T>> methodCall);

		string Enqueue([NotNull] Expression<Func<Task>> methodCall);

		string Enqueue([NotNull] string queue, [NotNull] Expression<Func<Task>> methodCall);
	}
}
