using Framework.Query.Mediator;
using Marketplace.Queries.Contracts.ClassifiedAds.Filters;
using Marketplace.Queries.Contracts.ClassifiedAds.Results;

namespace Marketplace.Queries.ClassifiedAds
{
	internal class GetPublicClassifiedAdQueryHandler : IQueryHandler<GetPublicClassifiedAdQueryFilter, List<ClassifiedAdItemResult>>
	{
		public GetPublicClassifiedAdQueryHandler()
		{
		}

		public Task<List<ClassifiedAdItemResult>> Handle(GetPublicClassifiedAdQueryFilter request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
