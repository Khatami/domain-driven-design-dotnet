namespace Marketplace.Application.Contracts.ClassifiedAds.Commands.V1
{
    public class SetClassifiedAdTitle
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
}