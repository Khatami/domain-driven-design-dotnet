using Marketplace.Application.Infrastructure.Mediator;
using Marketplace.Queries.Contracts.ClassifiedAds.QueryFilters;
using Marketplace.Queries.Contracts.ClassifiedAds.QueryResults;

namespace Marketplace.Queries.EF.ClassifiedAds.QueryHandlers
{
	internal class GetPublishedAdsQueryHandler : IQueryHandler<GetPublishedAdsQueryFilter, List<ClassifiedAdItem>>
	{
		public GetPublishedAdsQueryHandler()
		{
		}

		public async Task<List<ClassifiedAdItem>> Handle(GetPublishedAdsQueryFilter request, CancellationToken cancellationToken)
		{
			return new List<ClassifiedAdItem>();
		}
	}
}
