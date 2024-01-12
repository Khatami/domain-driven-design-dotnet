using Framework.Query.Mediator;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Mediator.MediatR;

internal class QueryHandlerAdapter<TRequest, TResponse> : IRequestHandler<RequestAdapter<TRequest, TResponse>, TResponse> where TRequest : IQuery<TResponse>
{
	private readonly IQueryHandler<TRequest, TResponse> _queryHandler;
	public QueryHandlerAdapter(IQueryHandler<TRequest, TResponse> queryHandler)
	{
		_queryHandler = queryHandler;
	}

	public Task<TResponse> Handle(RequestAdapter<TRequest, TResponse> request, CancellationToken cancellationToken)
	{
		return _queryHandler.Handle(request.Value, cancellationToken);
	}
}