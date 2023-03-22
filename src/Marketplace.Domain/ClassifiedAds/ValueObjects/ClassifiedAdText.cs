namespace Marketplace.Domain.ClassifiedAds.ValueObjects
{
	public record ClassifiedAdText
	{
		// Factory Pattern
		private ClassifiedAdText(string text)
		{
			if (text.Length > 100)
				throw new ArgumentOutOfRangeException("Text cannot be longer than 100 characters", nameof(text));

			Text = text;
		}

		public string Text { get; }

		public static ClassifiedAdText FromString(string text) => new ClassifiedAdText(text);
	}
}
