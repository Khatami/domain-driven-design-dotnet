using MediatR;

namespace Marketplace.Mediator
{
	internal class RequestAdapter<TRequest, TResponse> : IRequest<TResponse>
	{
		public RequestAdapter(TRequest value)
		{
			Value = value;
		}

		public TRequest Value { get; }
	}
}