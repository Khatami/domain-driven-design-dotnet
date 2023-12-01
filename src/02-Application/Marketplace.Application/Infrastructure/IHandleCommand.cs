namespace Marketplace.Application.Infrastructure
{
	public interface IHandleCommand<T>
	{
		Task Handle(T command);
	}
}
