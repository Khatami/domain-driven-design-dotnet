using System;

namespace Marketplace.Domain.ClassifiedAds.ValueObjects
{
	public record UserId
	{
		private UserId() { }

		public UserId(Guid userId)
		{
			if (userId == default)
				throw new ArgumentException("OwnerId must be set", nameof(userId));

			Id = userId;
		}

		public Guid Id { get; private set; }

		public static implicit operator Guid(UserId self)
		{
			return self.Id;
		}
	}
}
