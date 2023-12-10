using Marketplace.Application.Infrastructure.Mediator;
using Marketplace.Queries.Contracts.ClassifiedAds.QueryResults;

namespace Marketplace.Queries.Contracts.ClassifiedAds.QueryFilters
{
	public class GetOwnerClassifiedAdQueryFilter : IQuery<List<ClassifiedAdItem>>
	{
		public Guid OwnerId { get; set; }
		public int Page { get; set; }
		public int PageSize { get; set; }
	}
}
