using Marketplace.Application.SeedWork.Mediator;
using Marketplace.Queries.Contracts.ClassifiedAds.QueryResults;

namespace Marketplace.Queries.Contracts.ClassifiedAds.QueryFilters
{
	public class GetPublishedAdsQueryFilter : IQuery<List<ClassifiedAdItem>>
	{
		public int Page { get; set; }

		public int pageSize { get; set; }
	}
}
