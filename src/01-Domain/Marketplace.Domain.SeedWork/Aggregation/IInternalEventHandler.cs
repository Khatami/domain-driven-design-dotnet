namespace Marketplace.Domain.SeedWork.Aggregation
{
	public interface IInternalEventHandler
	{
		void Handle(object @event);
	}
}
