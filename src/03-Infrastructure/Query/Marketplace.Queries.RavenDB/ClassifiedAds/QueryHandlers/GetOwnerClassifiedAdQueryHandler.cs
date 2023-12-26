using Marketplace.Application.SeedWork.Mediator;
using Marketplace.Queries.Contracts.ClassifiedAds.QueryFilters;
using Marketplace.Queries.Contracts.ClassifiedAds.QueryResults;

namespace Marketplace.Queries.RavenDB.ClassifiedAds.QueryHandlers
{
	internal class GetOwnerClassifiedAdQueryHandler : IQueryHandler<GetOwnerClassifiedAdQueryFilter, List<ClassifiedAdItem>>
    {
		public GetOwnerClassifiedAdQueryHandler()
		{
		}

		public Task<List<ClassifiedAdItem>> Handle(GetOwnerClassifiedAdQueryFilter request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
