namespace Framework.Domain.Aggregation
{
	public interface IInternalEventHandler
	{
		void Handle(object @event);
	}
}
