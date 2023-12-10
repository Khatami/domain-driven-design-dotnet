namespace Marketplace.Query.ClassifiedAd.Models
{
    public class ClassifiedAdDetails
    {
        public Guid ClassifiedAdId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string CurrencyCode { get; set; }
        public string Description { get; set; }
        public string SellersDisplayName { get; set; }
        public string[] PhotoUrls { get; set; }
    }
}