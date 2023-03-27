namespace Marketplace.Application.Contracts.ClassifiedAds.Commands.V1
{
    public class CreateClassifiedAd_V1
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
    }
}