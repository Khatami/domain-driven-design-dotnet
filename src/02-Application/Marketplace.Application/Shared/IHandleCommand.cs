namespace Marketplace.Application.Helpers
{
	public interface IHandleCommand<T>
	{
		Task Handle(T command);
	}
}
