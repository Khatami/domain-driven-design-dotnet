namespace Marketplace.Domain.SeedWork.Aggregation
{
	public abstract class AggregateRoot<TId>
	{
		private readonly List<object> _changes;

		protected AggregateRoot()
		{
			_changes = new List<object>();
		}

		public TId Id { get; protected set; }

		public int Version { get; private set; } = -1;

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

		public void Load(IEnumerable<object> history)
		{
			foreach (var e in history)
			{
				When(e);
				Version++;
			}
		}

		public void ClearChanges() => _changes.Clear();

		protected abstract void EnsureValidState();

		// We use this method to apply only changes to the entity, because the event has been raised in the aggregate root and it has been added to _changes
		protected void ApplyToEntity(IInternalEventHandler entity, object @event)
		{
			entity?.Handle(@event);
		}
	}
}
