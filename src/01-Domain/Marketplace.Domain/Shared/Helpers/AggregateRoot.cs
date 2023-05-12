namespace Marketplace.Domain.Shared.Helpers
{
    public abstract class AggregateRoot<TId>
    {
        private readonly List<object> _changes;

        protected AggregateRoot()
        {
            _changes = new List<object>();
        }

        public TId Id { get; protected set; }

        protected void Apply(object @event)
        {
            When(@event);
            EnsureValidState();
            _changes.Add(@event);
        }

        protected abstract void When(object @event);

        public IEnumerable<object> GetChanges()
        {
            return _changes.AsEnumerable();
        }

        public void ClearChanges() => _changes.Clear();

        protected abstract void EnsureValidState();

        // We use this method to apply only changes to the entity, becase the event has been raised in the aggregate root and it has been added to _changes
        protected void ApplyToEntity(IInternalEventHandler entity, object @event)
        {
            entity?.Handle(@event);
        }
    }
}
