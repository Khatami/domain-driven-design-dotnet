namespace Marketplace.Domain.ClassifiedAds.Metadata
{
	public class Currency
	{
		public string CurrencyCode { get; set; }

		public bool IsUse { get; set; }

		public int DecimalPoints { get; set; }


		public static Currency None = new Currency { IsUse = false };
	}
}