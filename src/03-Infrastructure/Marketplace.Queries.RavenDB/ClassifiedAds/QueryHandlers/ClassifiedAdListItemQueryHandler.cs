using Marketplace.Application.Infrastructure.Mediator;
using Marketplace.Queries.Contracts.ClassifiedAds.QueryFilters;
using Marketplace.Queries.Contracts.ClassifiedAds.QueryResults;

namespace Marketplace.Queries.RavenDB.ClassifiedAds.QueryHandlers
{
	internal class ClassifiedAdListItemQueryHandler : IQueryHandler<GetPublishedAdsQueryFilter, List<ClassifiedAdItem>>
    {
        public async Task<List<ClassifiedAdItem>> Handle(GetPublishedAdsQueryFilter request, CancellationToken cancellationToken)
        {
            return new List<ClassifiedAdItem>();
        }
    }
}
