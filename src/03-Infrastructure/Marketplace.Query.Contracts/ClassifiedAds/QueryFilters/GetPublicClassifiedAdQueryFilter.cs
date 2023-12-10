using Marketplace.Application.Infrastructure.Mediator;
using Marketplace.Query.Contracts.ClassifiedAds.QueryResults;

namespace Marketplace.Query.Contracts.ClassifiedAds.QueryFilters
{
	public class GetPublicClassifiedAdQueryFilter : IQuery<List<ClassifiedAdItem>>
	{
		public Guid ClassifiedAdId { get; set; }
	}
}
