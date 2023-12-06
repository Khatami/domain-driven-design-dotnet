namespace Marketplace.Mediator;

public interface IQueryHandler<in TQuery, TReturnValue> :
	MediatR.IRequestHandler<TQuery, TReturnValue> where TQuery : MediatR.IRequest<TReturnValue>
{
}
