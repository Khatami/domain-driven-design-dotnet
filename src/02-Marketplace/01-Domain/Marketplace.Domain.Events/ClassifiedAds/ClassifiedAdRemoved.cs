namespace Marketplace.Domain.Events.ClassifiedAds
{
	public record ClassifiedAdRemoved(Guid Id, DateTimeOffset DeletedOn);
}
