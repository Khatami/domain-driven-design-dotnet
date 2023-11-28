namespace Marketplace.Domain.ClassifiedAds.ValueObjects
{
	public record ClassifiedAdText
	{
		private ClassifiedAdText() { }

		// Factory Pattern
		private ClassifiedAdText(string text)
		{
			if (text.Length > 100)
				throw new ArgumentOutOfRangeException("Text cannot be longer than 100 characters", nameof(text));

			Text = text;
		}

		public string Text { get; private set; }

		public static ClassifiedAdText FromString(string text) => new ClassifiedAdText(text);

		public static implicit operator string(ClassifiedAdText self)
		{
			return self.Text;
		}
	}
}
