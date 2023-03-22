using System;

namespace Marketplace.Domain.ClassifiedAd.ValueObjects
{
	public record ClassifiedAdId
	{
		public ClassifiedAdId(Guid value)
		{
			if (value == default)
				throw new ArgumentException("OwnerId must be set", nameof(value));

			Value = value;
		}

		public Guid Value { get; }
	}
}
