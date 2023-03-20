using System;

namespace Marketplace.Domain.ClassifiedAds.ValueObjects
{
	//public class Money : IEquatable<Money>
	//{
	//	public decimal Amount { get; }

	//	public Money(decimal amount)
	//	{
	//		Amount = amount;
	//	}

	//	public bool Equals(Money other)
	//	{
	//		if (ReferenceEquals(other, null)) { return false; }
	//		if (ReferenceEquals(this, other)) { return true; }
	//		if (this.Amount == other.Amount) { return true; }

	//		return false;
	//	}

	//	public override bool Equals(object obj)
	//	{
	//		if (obj.GetType() != typeof(Money)) { return false; }

	//		return Equals((Money)obj);
	//	}

	//	public override int GetHashCode() => Amount.GetHashCode();

	//	public static bool operator == (Money left, Money right)
	//	{
	//		return left.Equals(right);
	//	}

	//	public static bool operator != (Money left, Money right)
	//	{
	//		return !left.Equals(right);
	//	}
	//}

	// Because record has all features above, all tests are going to run perfectly.
	// public record Money(decimal Amount);

	// We can extend record as much as you want
	public record Money
	{
		public Money(decimal amount)
		{
			if (amount == default)
				throw new ArgumentException("OwnerId must be set", nameof(amount));

			Amount = amount;
		}

		public decimal Amount { get; }

		public Money Add(Money money)
		{
			return new Money(money.Amount + this.Amount);
		}

		public Money Subtract(Money money)
		{
			return new Money(money.Amount - this.Amount);
		}

		public static Money operator + (Money arg1, Money arg2)
		{
			return arg1.Add(arg2);
		}

		public static Money operator -(Money arg1, Money arg2)
		{
			return arg1.Subtract(arg2);
		}
	}
}
