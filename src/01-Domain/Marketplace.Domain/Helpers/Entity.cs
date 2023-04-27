﻿using System.Security.Cryptography;

namespace Marketplace.Domain.Helpers
{
	public abstract class Entity<TId>
	{
		private readonly List<object> _changes;

		protected Entity()
		{
			_changes = new List<object>();
		}

		public TId Id { get; protected set; }

		protected void Apply(object @event)
		{
			When(@event);
			_changes.Add(@event);
		}

		protected abstract void When(object @event);

		public IEnumerable<object> GetChanges()
		{
			return _changes.AsEnumerable();
		}

		public void ClearChanges() => _changes.Clear();
	}
}
