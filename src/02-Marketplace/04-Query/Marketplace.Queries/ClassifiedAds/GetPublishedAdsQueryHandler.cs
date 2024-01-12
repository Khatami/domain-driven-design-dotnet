using Framework.Query.Mediator;
using Marketplace.Queries.Contracts.ClassifiedAds.Filters;
using Marketplace.Queries.Contracts.ClassifiedAds.Results;

namespace Marketplace.Queries.ClassifiedAds
{
	internal class GetPublishedAdsQueryHandler : IQueryHandler<GetPublishedAdsQueryFilter, List<ClassifiedAdItemResult>>
	{
		public GetPublishedAdsQueryHandler()
		{
		}

		public async Task<List<ClassifiedAdItemResult>> Handle(GetPublishedAdsQueryFilter request, CancellationToken cancellationToken)
		{
			return new List<ClassifiedAdItemResult>();
		}
	}
}
