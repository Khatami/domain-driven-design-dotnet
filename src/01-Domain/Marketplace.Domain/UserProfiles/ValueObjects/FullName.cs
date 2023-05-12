namespace Marketplace.Domain.UserProfiles.ValueObjects
{
	public record FullName
	{
		public string Value { get; }

		private FullName(string value)
		{
			Value = value;
		}

		public static FullName FromString(string fullName)
		{
			if (string.IsNullOrWhiteSpace(fullName) == false)
				throw new ArgumentNullException(nameof(fullName));

			return new FullName(fullName);
		}

		public static implicit operator string(FullName fullName)
		{
			return fullName.Value;
		}
	}
}
