namespace Marketplace.Application.Contracts.ClassifiedAds.Commands.V1
{
    public class ClassifiedAd_UpdateText_V1
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
    }
}