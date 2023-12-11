namespace Marketplace.Domain.Shared.ValueObjects
{
    public record UserProfileId
    {
        private UserProfileId() { }

        public UserProfileId(Guid userId)
        {
            if (userId == default)
                throw new ArgumentException("OwnerId must be set", nameof(userId));

            Value = userId;
        }

        public Guid Value { get; private set; }

        public static implicit operator Guid(UserProfileId self)
        {
            return self.Value;
        }

		public static implicit operator string(UserProfileId self)
		{
			return self.Value.ToString();
		}
	}
}
