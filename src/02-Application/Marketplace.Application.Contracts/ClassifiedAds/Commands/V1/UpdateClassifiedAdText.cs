namespace Marketplace.Application.Contracts.ClassifiedAds.Commands.V1
{
    public class UpdateClassifiedAdText
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
    }
}