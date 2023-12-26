using MediatR;

namespace Marketplace.Mediator.MediatR
{
	internal class RequestAdapter<TRequest, TResponse> : IRequest<TResponse>
	{
		public RequestAdapter(TRequest value)
		{
			Value = value;
		}

		public TRequest Value { get; }
	}

	internal class RequestUnitAdapter<TRequest> : IRequest
	{
		public RequestUnitAdapter(TRequest value)
		{
			Value = value;
		}

		public TRequest Value { get; }
	}
}