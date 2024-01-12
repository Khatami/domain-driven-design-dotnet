using Framework.Query.Mediator;
using Marketplace.Queries.Contracts.ClassifiedAds.Filters;
using Marketplace.Queries.Contracts.ClassifiedAds.Results;

namespace Marketplace.Queries.ClassifiedAds
{
	internal class GetOwnerClassifiedAdQueryHandler : IQueryHandler<GetOwnerClassifiedAdQueryFilter, List<ClassifiedAdItemResult>>
	{
		public GetOwnerClassifiedAdQueryHandler()
		{
		}

		public Task<List<ClassifiedAdItemResult>> Handle(GetOwnerClassifiedAdQueryFilter request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
