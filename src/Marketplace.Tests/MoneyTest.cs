using Marketplace.Domain.ClassifiedAd.ValueObjects;

namespace Marketplace.Tests
{
	public class MoneyTest
	{
		[Fact]
		public void Money_objects_with_the_same_amount_should_be_equal()
		{
			var firstAmount = Money.FromDecimal(5);
			var secondAmount = Money.FromDecimal(5);

			Assert.Equal(firstAmount, secondAmount);
		}

		[Fact]
		public void Sum_of_money_gives_full_amount()
		{
			var firstAmount = Money.FromDecimal(5);
			var secondAmount = Money.FromDecimal(10);
			var thirdAmount = Money.FromDecimal(15);

			Assert.Equal(firstAmount + secondAmount, thirdAmount);
		}
	}
}
