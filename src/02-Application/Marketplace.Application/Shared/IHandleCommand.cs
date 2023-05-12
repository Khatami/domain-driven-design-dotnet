namespace Marketplace.Application.Shared
{
	public interface IHandleCommand<T>
	{
		Task Handle(T command);
	}
}
