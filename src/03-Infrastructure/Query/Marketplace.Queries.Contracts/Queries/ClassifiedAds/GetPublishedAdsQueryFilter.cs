using Marketplace.Application.SeedWork.Mediator;
using Marketplace.Queries.Contracts.ReadModels.ClassifiedAds;

namespace Marketplace.Queries.Contracts.Queries.ClassifiedAds
{
    public class GetPublishedAdsQueryFilter : IQuery<List<ClassifiedAdItem>>
    {
        public int Page { get; set; }

        public int pageSize { get; set; }
    }
}
