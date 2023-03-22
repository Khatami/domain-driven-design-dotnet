﻿namespace Marketplace.Domain.ClassifiedAd.Exceptions
{
	public class InvalidEntityStateException : Exception
	{
		public InvalidEntityStateException(object entity, string message)
			: base(message: $"Entity {entity.GetType().Name} state change rejected, {message}")
		{
		}
	}
}
