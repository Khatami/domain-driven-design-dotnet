using Marketplace.Domain.ClassifiedAd.DomainServices;
using Marketplace.Domain.ClassifiedAd.ValueObjects;

namespace Marketplace.Tests.ClassifiedAd
{
    public class MoneyTest
    {
        private ICurrencyLookup _currencyLookup = new FakeCurrencyLookup();

        [Fact]
        public void Money_objects_with_the_same_amount_should_be_equal()
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
    }
}
