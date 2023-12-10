using Marketplace.Application.Infrastructure.Mediator;
using Marketplace.Query.Contracts.ClassifiedAds.QueryResults;

namespace Marketplace.Query.Contracts.ClassifiedAds.QueryFilters
{
	public class GetPublishedAdsQueryFilter : IQuery<List<ClassifiedAdItem>>
	{
		public int Page { get; set; }

		public int pageSize { get; set; }
	}
}
