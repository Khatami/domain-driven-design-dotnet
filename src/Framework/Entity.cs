﻿namespace Framework
{
	public abstract class Entity
	{
		private readonly List<Object> _events;

		protected Entity()
		{
			_events = new List<object>();
		}

		protected void Apply(object @event)
		{
			When(@event);
			EnsureValidState();
			_events.Add(@event);
		}

		protected abstract void When(object @event);

		public IEnumerable<object> GetChanges()
		{
			return _events.AsEnumerable();
		}

		public void ClearChanges() => _events.Clear();

		protected abstract void EnsureValidState();
	}
}
