namespace Marketplace.Application.Contracts.ClassifiedAds.Queries.QueryModels;

public class GetOwnersClassifiedAdQuery
{
	public Guid OwnerId { get; set; }
	public int Page { get; set; }
	public int PageSize { get; set; }
}
