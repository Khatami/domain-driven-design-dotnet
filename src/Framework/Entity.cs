namespace Framework
{
	public abstract class Entity
	{
		private readonly List<Object> _events;

		protected Entity()
		{
			_events = new List<object>();
		}

		protected void Raise(object @event)
		{
			_events.Add(@event);
		}

		public IEnumerable<object> GetChanges()
		{
			return _events.AsEnumerable();
		}

		public void ClearChanges() => _events.Clear();
	}
}
