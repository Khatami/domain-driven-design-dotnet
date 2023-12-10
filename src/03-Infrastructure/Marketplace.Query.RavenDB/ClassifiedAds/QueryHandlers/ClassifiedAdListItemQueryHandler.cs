using Marketplace.Application.Infrastructure.Mediator;
using Marketplace.Query.Contracts.ClassifiedAds.QueryFilters;
using Marketplace.Query.Contracts.ClassifiedAds.QueryResults;

namespace Marketplace.Query.RavenDB.ClassifiedAds.QueryHandlers
{
	internal class ClassifiedAdListItemQueryHandler : IQueryHandler<GetPublishedAdsQueryFilter, List<ClassifiedAdItem>>
    {
        public async Task<List<ClassifiedAdItem>> Handle(GetPublishedAdsQueryFilter request, CancellationToken cancellationToken)
        {
            return new List<ClassifiedAdItem>();
        }
    }
}
