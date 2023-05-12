namespace Marketplace.Domain.Shared.Helpers
{
    public interface IInternalEventHandler
    {
        void Handle(object @event);
    }
}
