namespace Marketplace.Application.Contracts.ClassifiedAds.Commands.V1
{
    public class UpdateClassifiedAdPrice_V1
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
    }
}