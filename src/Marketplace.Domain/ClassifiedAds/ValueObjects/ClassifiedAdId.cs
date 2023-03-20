using System;

namespace Marketplace.Domain.ClassifiedAds.ValueObjects
{
    public class ClassifiedAdId
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
