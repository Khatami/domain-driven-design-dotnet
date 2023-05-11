using System.Security.Cryptography;

namespace Marketplace.Domain.Helpers
{
    public abstract class Entity<TId> : IInternalEventHandler
    {
        private readonly Action<object> _applier;

        protected Entity() { }

        protected Entity(Action<object> applier)
        {
            _applier = applier;
        }

        public TId Id { get; protected set; }

        protected abstract void When(object entity);

        public void Apply(object @event)
        {
            When(@event);
            _applier(@event);
        }

        void IInternalEventHandler.Handle(object @event)
        {
            When(@event);
        }
    }
}
