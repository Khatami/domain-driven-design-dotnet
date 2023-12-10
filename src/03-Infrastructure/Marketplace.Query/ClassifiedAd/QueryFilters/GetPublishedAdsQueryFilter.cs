using Marketplace.Application.Infrastructure.Mediator;
using Marketplace.Query.ClassifiedAd.Models;

namespace Marketplace.Query.ClassifiedAd.QueryFilters
{
	public class GetPublishedAdsQueryFilter : IQuery<List<ClassifiedAdItem>>
	{
        public int Page { get; set; }

        public int pageSize { get; set; }
    }
}
