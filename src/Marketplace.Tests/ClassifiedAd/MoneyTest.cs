﻿using Marketplace.Domain.ClassifiedAd.DomainServices;
using Marketplace.Domain.ClassifiedAd.Exceptions;
using Marketplace.Domain.ClassifiedAd.ValueObjects;

namespace Marketplace.Tests.ClassifiedAd
{
    public class MoneyTest
    {
        private ICurrencyLookup _currencyLookup = new FakeCurrencyLookup();

        [Fact]
        public void Two_of_same_amount_should_be_equal()
        {
            var firstAmount = Money.FromDecimal(5, "EUR", _currencyLookup);
            var secondAmount = Money.FromDecimal(5, "EUR", _currencyLookup);

            Assert.Equal(firstAmount, secondAmount);
        }

        [Fact]
        public void Two_of_same_amount_but_differentCurrencies_should_not_eb_equal()
        {
			var firstAmount = Money.FromDecimal(5, "EUR", _currencyLookup);
			var secondAmount = Money.FromDecimal(5, "USD", _currencyLookup);

			Assert.NotEqual(firstAmount, secondAmount);
		}

        [Fact]
        public void FromString_and_fromDecimal_should_be_equal()
        {
			var firstAmount = Money.FromDecimal(5, "EUR", _currencyLookup);
			var secondAmount = Money.FromDecimal(5, "EUR", _currencyLookup);

			Assert.Equal(firstAmount, secondAmount);
		}

        [Fact]
        public void Sum_of_money_gives_full_amount()
        {
            var firstAmount = Money.FromDecimal(5, "EUR", _currencyLookup);
            var secondAmount = Money.FromDecimal(10, "EUR", _currencyLookup);
            var thirdAmount = Money.FromDecimal(15, "EUR", _currencyLookup);

            Assert.Equal(firstAmount + secondAmount, thirdAmount);
        }

        [Fact]
        public void Unused_currency_should_not_be_allowed()
        {
            Assert.Throws<ArgumentException>(() => Money.FromDecimal(5, "DEM", _currencyLookup));
        }

		[Fact]
		public void Unknwon_currency_should_not_be_allowed()
		{
			Assert.Throws<ArgumentException>(() => Money.FromDecimal(5, "WHEN", _currencyLookup));
		}

		[Fact]
		public void Throw_when_too_many_decimal_places()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => Money.FromDecimal(5.123M, "EUR", _currencyLookup));
		}

        [Fact]
        public void Throws_on_adding_different_currencies()
        {
            var firstAmount = Money.FromDecimal(5, "EUR", _currencyLookup);
			var secondAmount = Money.FromDecimal(5, "USD", _currencyLookup);

            Assert.Throws<CurrencyMismatchException>(() => firstAmount + secondAmount);
		}

		[Fact]
		public void Throws_on_subtracting_different_currencies()
		{
			var firstAmount = Money.FromDecimal(5, "EUR", _currencyLookup);
			var secondAmount = Money.FromDecimal(5, "USD", _currencyLookup);

			Assert.Throws<CurrencyMismatchException>(() => firstAmount - secondAmount);
		}
	}
}
