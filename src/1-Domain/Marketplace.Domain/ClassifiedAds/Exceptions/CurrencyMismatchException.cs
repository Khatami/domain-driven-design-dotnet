namespace Marketplace.Domain.ClassifiedAds.Exceptions
{
	public class CurrencyMismatchException : Exception
	{
		public CurrencyMismatchException(string message) : base(message)
		{ }
	}
}
