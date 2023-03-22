using Marketplace.Domain.ClassifiedAds.Arguments;
using Marketplace.Domain.ClassifiedAds.DomainServices;

namespace Marketplace.Domain.ClassifiedAds.ValueObjects
{
	public record Price : Money
	{
		public Price(MoneyArguments moneyArguments) : base(moneyArguments)
		{
			if (moneyArguments.Amount < 0)
				throw new ArgumentException("Price cannot be negative", nameof(moneyArguments.Amount));
		}

		public new static Price FromDecimal(MoneyArguments decimalMoneyArguments) => new Price(decimalMoneyArguments);

		public new static Price FromString(string amount, string currency, ICurrencyLookup currencyLookup) => new Price(new MoneyArguments(decimal.Parse(amount), currency, currencyLookup));
	}
}
