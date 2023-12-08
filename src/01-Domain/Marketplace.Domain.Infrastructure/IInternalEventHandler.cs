namespace Marketplace.Domain.Infrastructure
{
    public interface IInternalEventHandler
    {
        void Handle(object @event);
    }
}
