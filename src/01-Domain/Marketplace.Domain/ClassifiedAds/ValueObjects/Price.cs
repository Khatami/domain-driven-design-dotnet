using Marketplace.Domain.ClassifiedAds.Arguments;
using Marketplace.Domain.ClassifiedAds.DomainServices;
using Marketplace.Domain.ClassifiedAds.Metadata;

namespace Marketplace.Domain.ClassifiedAds.ValueObjects
{
	public record Price : Money
	{
		private Price() { }

		public Price(MoneyArguments moneyArguments) : base(moneyArguments)
		{
			if (moneyArguments.Amount < 0)
				throw new ArgumentException("Price cannot be negative", nameof(moneyArguments.Amount));
		}

		internal Price(decimal amount, string currency) : base(amount, new Currency() { CurrencyCode = currency })
		{
		}

		public new static Price FromDecimal(MoneyArguments decimalMoneyArguments) => new Price(decimalMoneyArguments);

		public new static Price FromString(string amount, string currency, ICurrencyLookup currencyLookup) => new Price(new MoneyArguments(decimal.Parse(amount), currency, currencyLookup));
	}
}
