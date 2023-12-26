using Marketplace.Application.SeedWork.Mediator;
using Marketplace.Queries.Contracts.ClassifiedAds.QueryResults;

namespace Marketplace.Queries.Contracts.ClassifiedAds.QueryFilters
{
	public class GetPublicClassifiedAdQueryFilter : IQuery<List<ClassifiedAdItem>>
	{
		public Guid ClassifiedAdId { get; set; }
	}
}
