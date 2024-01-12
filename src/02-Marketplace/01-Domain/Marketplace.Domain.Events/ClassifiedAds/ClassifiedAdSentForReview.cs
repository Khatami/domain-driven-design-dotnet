namespace Marketplace.Domain.Events.ClassifiedAds
{
	public record ClassifiedAdSentForReview(Guid Id, Guid ApprovedById);
}
