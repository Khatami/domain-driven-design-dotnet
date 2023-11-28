using Marketplace.Domain.UserProfiles.Delegates;
using Marketplace.Domain.UserProfiles.Exceptions;

namespace Marketplace.Domain.UserProfiles.ValueObjects
{
    public record DisplayName
	{
		private DisplayName() { }

		private DisplayName(string displayName)
		{
			Value = displayName;
		}

		public string Value { get; }

		public static DisplayName FromString(string displayName, CheckTextForProfanity checkTextForProfanity)
		{
			if (string.IsNullOrWhiteSpace(displayName))
			{
				throw new ArgumentNullException(nameof(displayName));
			}

			if (checkTextForProfanity(displayName))
			{
				throw new ProfanityFoundException(displayName);
			}

			return new DisplayName(displayName);
		}

		public static implicit operator string(DisplayName displayName)
		{
			return displayName.Value;
		}
	}
}
