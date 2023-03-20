using System;

namespace Marketplace.Domain.ClassifiedAd.ValueObjects
{
	public record ClassifiedAdId
	{
		private readonly Guid _value;

		public ClassifiedAdId(Guid value)
		{
			if (value == default)
				throw new ArgumentException("OwnerId must be set", nameof(value));

			_value = value;
		}
	}
}
