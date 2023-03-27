namespace Marketplace.Application.Contracts.ClassifiedAds.Commands.V1
{
    public class UpdateClassifiedAdText_V1
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
    }
}