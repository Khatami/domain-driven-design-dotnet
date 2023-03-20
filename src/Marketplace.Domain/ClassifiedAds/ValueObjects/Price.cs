namespace Marketplace.Domain.ClassifiedAds.ValueObjects
{
	public record Price: Money
	{
		public Price(decimal amount): base(amount)
		{
			if (amount < 0)
				throw new ArgumentException("Price cannot be negative", nameof(amount));
		}
	}
}
