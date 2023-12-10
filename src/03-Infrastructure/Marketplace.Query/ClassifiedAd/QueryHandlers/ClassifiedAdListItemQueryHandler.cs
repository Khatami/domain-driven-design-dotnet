using Marketplace.Application.Infrastructure.Mediator;
using Marketplace.Query.ClassifiedAd.Models;
using Marketplace.Query.ClassifiedAd.QueryFilters;

namespace Marketplace.Query.ClassifiedAd.QueryHandlers
{
	internal class ClassifiedAdListItemQueryHandler : IQueryHandler<GetPublishedAdsQueryFilter, List<ClassifiedAdItem>>
	{
		public async Task<List<ClassifiedAdItem>> Handle(GetPublishedAdsQueryFilter request, CancellationToken cancellationToken)
		{
			return new List<ClassifiedAdItem>();
		}
	}
}
