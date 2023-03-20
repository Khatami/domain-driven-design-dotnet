using System;

namespace Marketplace.Domain.ClassifiedAd.ValueObjects
{
	public record UserId
	{
		private readonly Guid _userId;

		public UserId(Guid userId)
		{
			if (userId == default)
				throw new ArgumentException("OwnerId must be set", nameof(userId));

			_userId = userId;
		}
	}
}
