using Marketplace.Application.Infrastructure.Mediator;
using Marketplace.Query.Contracts.ClassifiedAds.QueryResults;

namespace Marketplace.Query.Contracts.ClassifiedAds.QueryFilters
{
	public class GetOwnerClassifiedAdQueryFilter : IQuery<List<ClassifiedAdItem>>
	{
		public Guid OwnerId { get; set; }
		public int Page { get; set; }
		public int PageSize { get; set; }
	}
}
