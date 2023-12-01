using Polly;
using Polly.Retry;

namespace Marketplace.Application.Infrastructure
{
	internal class RetryingCommandHandler<T> : IHandleCommand<T>
	{
		static RetryPolicy _policy = Policy
			.Handle<InvalidOperationException>()
			.Retry();

		private IHandleCommand<T> _next;
		public RetryingCommandHandler(IHandleCommand<T> next)
		{
			_next = next;
		}

		public Task Handle(T command)
		{
			return _policy.Execute(() => _next.Handle(command));
		}
	}
}
