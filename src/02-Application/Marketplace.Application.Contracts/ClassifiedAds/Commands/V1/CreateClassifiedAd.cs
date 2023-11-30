namespace Marketplace.Application.Contracts.ClassifiedAds.Commands.V1
{
    public class CreateClassifiedAd
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
    }
}