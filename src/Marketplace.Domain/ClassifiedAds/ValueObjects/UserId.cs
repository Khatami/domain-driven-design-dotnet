using System;

namespace Marketplace.Domain.ClassifiedAds.ValueObjects
{
    public class UserId
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
