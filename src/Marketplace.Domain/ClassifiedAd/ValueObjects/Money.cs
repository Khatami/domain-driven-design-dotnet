using Marketplace.Domain.ClassifiedAd.Exceptions;

namespace Marketplace.Domain.ClassifiedAd.ValueObjects
{
	public record Money
	{
		public decimal Amount { get; }

		public string CurrencyCode { get; }

		protected Money(decimal amount, string currencyCode = "EUR")
		{
			if (amount == default)
				throw new ArgumentException("OwnerId must be set", nameof(amount));

			if (decimal.Round(amount, 2) != amount)
				throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot have more than two decimals");

			Amount = amount;
			CurrencyCode = currencyCode;
		}

		private const string DefaultCurrency = "EUR";

		public static Money FromDecimal(decimal amount, string currency = DefaultCurrency) => new Money(amount, currency);

		public static Money FromString(string amount, string currency = DefaultCurrency) => new Money(decimal.Parse(amount), currency);

		public Money Add(Money money)
		{
			if (money.CurrencyCode != CurrencyCode)
				throw new CurrencyMismatchException("Cannot sum amounts with different currencies");

			return new Money(money.Amount + Amount);
		}

		public Money Subtract(Money money)
		{
			if (money.CurrencyCode != CurrencyCode)
				throw new CurrencyMismatchException("Cannot subtract amounts with different currencies");

			return new Money(money.Amount - Amount);
		}

		public static Money operator +(Money arg1, Money arg2)
		{
			if (arg1.CurrencyCode != arg2.CurrencyCode)
				throw new CurrencyMismatchException("Cannot sum amounts with different currencies");

			return arg1.Add(arg2);
		}

		public static Money operator -(Money arg1, Money arg2)
		{
			if (arg1.CurrencyCode != arg2.CurrencyCode)
				throw new CurrencyMismatchException("Cannot subtract amounts with different currencies");

			return arg1.Subtract(arg2);
		}
	}
}
