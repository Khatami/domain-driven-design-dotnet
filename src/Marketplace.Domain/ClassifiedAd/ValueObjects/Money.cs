using Marketplace.Domain.ClassifiedAd.Arguments;
using Marketplace.Domain.ClassifiedAd.DomainServices;
using Marketplace.Domain.ClassifiedAd.Exceptions;
using Marketplace.Domain.ClassifiedAd.Metadata;

namespace Marketplace.Domain.ClassifiedAd.ValueObjects
{
    public record Money
	{
		protected Money(MoneyArguments moneyArguments)
		{
			if (string.IsNullOrWhiteSpace(moneyArguments.Currency))
				throw new ArgumentException("currencyCode must be set", nameof(moneyArguments.Currency));

			if (moneyArguments.Amount == default)
				throw new ArgumentException("OwnerId must be set", nameof(moneyArguments.Amount));

			var currency = moneyArguments.CurrencyLookup.FindCurrency(moneyArguments.Currency);

			if (currency.IsUse == false)
				throw new ArgumentException($"Currency {moneyArguments.Currency} is not valid");

			if (decimal.Round(moneyArguments.Amount, currency.DecimalPoints) != moneyArguments.Amount)
				throw new ArgumentOutOfRangeException(nameof(moneyArguments.Amount), "Amount cannot have more than two decimals");

			Amount = moneyArguments.Amount;
			Currency = currency;
		}

		private Money(decimal amount, CurrencyDetails currency)
		{
			Amount = amount;
			Currency = currency;
		}

		public static Money FromDecimal(MoneyArguments decimalMoneyArguments) => new Money(decimalMoneyArguments);

		public static Money FromString(string amount, string currency, ICurrencyLookup currencyLookup) => new Money(new MoneyArguments(decimal.Parse(amount), currency, currencyLookup));

		public Money Add(Money money)
		{
			if (money.Currency != Currency)
				throw new CurrencyMismatchException("Cannot sum amounts with different currencies");

			return new Money(money.Amount + Amount, money.Currency);
		}

		public Money Subtract(Money money)
		{
			if (money.Currency != Currency)
				throw new CurrencyMismatchException("Cannot subtract amounts with different currencies");

			return new Money(money.Amount - Amount, money.Currency);
		}

		public static Money operator +(Money arg1, Money arg2)
		{
			if (arg1.Currency != arg2.Currency)
				throw new CurrencyMismatchException("Cannot sum amounts with different currencies");

			return arg1.Add(arg2);
		}

		public static Money operator -(Money arg1, Money arg2)
		{
			if (arg1.Currency != arg2.Currency)
				throw new CurrencyMismatchException("Cannot subtract amounts with different currencies");

			return arg1.Subtract(arg2);
		}

		public override string ToString()
		{
			return $"{Currency.CurrencyCode} {Amount}";
		}

		public decimal Amount { get; }

		public CurrencyDetails Currency { get; }
	}
}
