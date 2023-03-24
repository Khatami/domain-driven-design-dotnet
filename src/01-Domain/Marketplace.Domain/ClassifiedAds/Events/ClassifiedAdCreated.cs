namespace Marketplace.Domain.ClassifiedAds.Events
{
	public record ClassifiedAdCreated(Guid Id, Guid OwnerId);
}
