using Framework.Domain.Aggregation.Exceptions;
using Framework.Domain.Comparison;
using Framework.Domain.Events;

namespace Framework.Domain.Aggregation
{
	public abstract class AggregateRootBase
	{
		private readonly List<object> _changes;

		internal AggregateRootBase()
		{
			_changes = new List<object>();
		}

		public abstract object GetId();

		public int Version { get; private set; } = -1;

		public int GetLatestVersion()
		{
			return Version + GetChanges().Count();
		}

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

		public void Snapshot(IComparisonService comparisonService)
		{
			var snapshotevent = GetSnapshotEvent();

			var newInstance = Activator.CreateInstance(GetType(), true);

			(newInstance as AggregateRootBase)!.Load(new object[] { snapshotevent });
			(newInstance as AggregateRootBase)!.Version = Version;

			if (comparisonService.Compare(this, newInstance) == false)
			{
				throw new SnapshotComparisonException();
			}

			_changes.Add(snapshotevent);
		}

		public abstract object GetSnapshotEvent();

		public bool CheckEventStreaming(AggregateRootBase latestPersistInstance, IComparisonService comparisonService)
		{
			latestPersistInstance.Load(this.GetChanges());
			latestPersistInstance.Version = Version;

			if (comparisonService.Compare(this, latestPersistInstance) == false)
			{
				return false;
			}

			return true;
		}
	}

	public abstract class AggregateRoot<TId> : AggregateRootBase
	{
		protected AggregateRoot() : base()
		{
		}

		public TId Id { get; protected set; }

		public void Remove()
		{
			var id = GetId().ToString()!;
			Apply(new AggregationRemoved(id));
		}
	}
}
