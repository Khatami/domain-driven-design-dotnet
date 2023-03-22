using System;

namespace Marketplace.Domain.ClassifiedAds.ValueObjects
{
	public record UserId
	{
		public UserId(Guid userId)
		{
			if (userId == default)
				throw new ArgumentException("OwnerId must be set", nameof(userId));

			Id = userId;
		}

		public Guid Id { get; }

		public static implicit operator Guid(UserId self)
		{
			return self.Id;
		}
	}
}
