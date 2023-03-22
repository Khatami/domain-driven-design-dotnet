using Marketplace.Domain.ClassifiedAd.DomainServices;
using Marketplace.Domain.ClassifiedAd.Exceptions;

namespace Marketplace.Domain.ClassifiedAd.ValueObjects
{
	public record Money
	{
		public decimal Amount { get; }

		public CurrencyDetails Currency { get; }

		protected Money(decimal amount, string currencyCode, ICurrencyLookup currencyLookup)
		{
			if (string.IsNullOrWhiteSpace(currencyCode))
				throw new ArgumentException("currencyCode must be set", nameof(currencyCode));

			if (amount == default)
				throw new ArgumentException("OwnerId must be set", nameof(amount));

			var currency = currencyLookup.FindCurrency(currencyCode);

			if (currency.IsUse == false)
				throw new ArgumentException($"Currency {currencyCode} is not valid");

			if (decimal.Round(amount, currency.DecimalPoints) != amount)
				throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot have more than two decimals");

			Amount = amount;
			Currency = currency;
		}

		private Money(decimal amount, CurrencyDetails currency)
		{
			Amount = amount;
			Currency = currency;
		}

		public static Money FromDecimal(decimal amount, string currency, ICurrencyLookup currencyLookup) => new Money(amount, currency, currencyLookup);

		public static Money FromString(string amount, string currency, ICurrencyLookup currencyLookup) => new Money(decimal.Parse(amount), currency, currencyLookup);

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
	}
}
