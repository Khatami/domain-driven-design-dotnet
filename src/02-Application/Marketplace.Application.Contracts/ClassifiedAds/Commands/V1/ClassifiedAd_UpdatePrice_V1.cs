namespace Marketplace.Application.Contracts.ClassifiedAds.Commands.V1
{
    public class ClassifiedAd_UpdatePrice_V1
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
    }
}