using Framework.Query.Mediator;
using Marketplace.Queries.Contracts.ClassifiedAds.Results;

namespace Marketplace.Queries.Contracts.ClassifiedAds.Filters
{
	public class GetPublishedAdsQueryFilter : IQuery<List<ClassifiedAdItemResult>>
	{
		public int Page { get; set; } = 1;

		public int PageSize { get; set; } = 10;
	}
}
