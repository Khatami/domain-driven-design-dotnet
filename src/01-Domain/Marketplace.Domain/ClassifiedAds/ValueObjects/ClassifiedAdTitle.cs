namespace Marketplace.Domain.ClassifiedAds.ValueObjects
{
	public record ClassifiedAdTitle
	{
		// Factory Pattern
		private ClassifiedAdTitle(string title)
		{
			if (title.Length > 100)
				throw new ArgumentOutOfRangeException("Title cannot be longer than 100 characters", nameof(title));

			Title = title;
		}

		public string Title { get; }

		public static ClassifiedAdTitle FromString(string title) => new ClassifiedAdTitle(title);

		public static implicit operator string(ClassifiedAdTitle self)
		{
			return self.ToString();
		}
	}
}
