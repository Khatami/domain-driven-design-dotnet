namespace Marketplace.Queries.Contracts.ClassifiedAds.Results
{
	public class ClassifiedAdItemResult
	{
		public Guid ClassifiedAdId { get; set; }
		public string Title { get; set; }
		public decimal Price { get; set; }
		public string CurrencyCode { get; set; }
		public string PhotoUrl { get; set; }
	}
}