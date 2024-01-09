using Marketplace.Application.SeedWork.Mediator;
using Marketplace.Queries.Contracts.Queries.ClassifiedAds;
using Marketplace.Queries.Contracts.ReadModels.ClassifiedAds;

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
