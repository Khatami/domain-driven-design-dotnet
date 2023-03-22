using System;

namespace Marketplace.Domain.ClassifiedAds.ValueObjects
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
