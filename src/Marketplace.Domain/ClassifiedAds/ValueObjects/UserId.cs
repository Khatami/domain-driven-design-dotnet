using System;

namespace Marketplace.Domain.ClassifiedAds.ValueObjects
{
	public record UserId
	{
		public UserId(Guid userId)
		{
			if (userId == default)
				throw new ArgumentException("OwnerId must be set", nameof(userId));

			_userId = userId;
		}

		public Guid _userId { get; }
	}
}
