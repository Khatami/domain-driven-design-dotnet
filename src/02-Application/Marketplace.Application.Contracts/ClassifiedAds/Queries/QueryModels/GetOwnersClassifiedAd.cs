namespace Marketplace.Application.Contracts.ClassifiedAds.Queries.QueryModels
{
	public class GetOwnersClassifiedAd
	{
		public Guid OwnerId { get; set; }
		public int Page { get; set; }
		public int PageSize { get; set; }
	}
}
