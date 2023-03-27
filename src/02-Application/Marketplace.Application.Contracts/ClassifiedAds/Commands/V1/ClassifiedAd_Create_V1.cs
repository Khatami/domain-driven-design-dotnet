namespace Marketplace.Application.Contracts.ClassifiedAds.Commands.V1
{
    public class ClassifiedAd_Create_V1
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
    }
}