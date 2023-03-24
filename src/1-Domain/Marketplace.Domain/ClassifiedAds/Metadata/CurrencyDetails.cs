namespace Marketplace.Domain.ClassifiedAds.Metadata
{
	public class CurrencyDetails
	{
		public string CurrencyCode { get; set; }

		public bool IsUse { get; set; }

		public int DecimalPoints { get; set; }


		public static CurrencyDetails None = new CurrencyDetails { IsUse = false };
	}
}