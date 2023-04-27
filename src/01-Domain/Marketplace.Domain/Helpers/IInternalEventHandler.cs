namespace Marketplace.Domain.Helpers
{
	public interface IInternalEventHandler
	{
		void Handle(object @event);
	}
}
