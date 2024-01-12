using Framework.Query.Mediator;
using Marketplace.Queries.Contracts.ClassifiedAds.Results;

namespace Marketplace.Queries.Contracts.ClassifiedAds.Filters
{
	public class GetPublishedAdsQueryFilter : IQuery<List<ClassifiedAdItemResult>>
	{
		public int Page { get; set; }

		public int pageSize { get; set; }
	}
}
