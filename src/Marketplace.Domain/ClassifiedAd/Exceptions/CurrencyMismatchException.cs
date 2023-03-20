namespace Marketplace.Domain.ClassifiedAd.Exceptions
{
	public class CurrencyMismatchException : Exception
	{
		public CurrencyMismatchException(string message) : base(message)
		{ }
	}
}
