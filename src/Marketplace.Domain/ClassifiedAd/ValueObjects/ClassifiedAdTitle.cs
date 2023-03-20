namespace Marketplace.Domain.ClassifiedAd.ValueObjects
{
	public record ClassifiedAdTitle
	{
		private readonly string _title;

		// Factory Pattern
		private ClassifiedAdTitle(string title)
		{
			if (title.Length > 100)
				throw new ArgumentOutOfRangeException("Title cannot be longer than 100 characters", nameof(title));

			_title = title;
		}

		public static ClassifiedAdTitle FromString(string title) => new ClassifiedAdTitle(title);
	}
}
