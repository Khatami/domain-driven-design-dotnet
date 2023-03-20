using Marketplace.Domain.ClassifiedAds.ValueObjects;

namespace Marketplace.Tests
{
	public class MoneyTest
	{
		[Fact]
		public void Money_objects_with_the_same_amount_should_be_equal()
		{
			var firstAmount = new Money(5);
			var secondAmount = new Money(5);

			Assert.Equal(firstAmount, secondAmount);
		}

		[Fact]
		public void Sum_of_money_gives_full_amount()
		{
			var firstAmount = new Money(5);
			var secondAmount = new Money(10);
			var thirdAmount = new Money(15);

			Assert.Equal(firstAmount + secondAmount, thirdAmount);
		}
	}
}
