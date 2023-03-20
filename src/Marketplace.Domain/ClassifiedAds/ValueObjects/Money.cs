using System;

namespace Marketplace.Domain.ClassifiedAds.ValueObjects
{
	public class Money : IEquatable<Money>
	{
		public decimal Amount { get; }

		public Money(decimal amount)
		{
			Amount = amount;
		}

		public bool Equals(Money other)
		{
			if (ReferenceEquals(other, null)) { return false; }
			if (ReferenceEquals(this, other)) { return true; }
			if (this.Amount == other.Amount) { return true; }

			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj.GetType() != typeof(Money)) { return false; }

			return Equals((Money)obj);
		}

		public override int GetHashCode() => Amount.GetHashCode();

		public static bool operator == (Money left, Money right)
		{
			return left.Equals(right);
		}

		public static bool operator != (Money left, Money right)
		{
			return !left.Equals(right);
		}
	}
}
