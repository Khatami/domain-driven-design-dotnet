using Framework.Query.Mediator;
using Marketplace.Queries.Contracts.ClassifiedAds.Results;

namespace Marketplace.Queries.Contracts.ClassifiedAds.Filters
{
	public class GetPublicClassifiedAdQueryFilter : IQuery<List<ClassifiedAdItemResult>>
	{
		public Guid ClassifiedAdId { get; set; }
	}
}
