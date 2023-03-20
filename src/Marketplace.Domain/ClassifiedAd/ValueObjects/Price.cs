namespace Marketplace.Domain.ClassifiedAd.ValueObjects
{
	public record Price : Money
	{
		public Price(decimal amount) : base(amount)
		{
			if (amount < 0)
				throw new ArgumentException("Price cannot be negative", nameof(amount));
		}
	}
}
