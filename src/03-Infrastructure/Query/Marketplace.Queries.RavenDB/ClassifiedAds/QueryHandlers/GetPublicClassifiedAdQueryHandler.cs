using Marketplace.Application.SeedWork.Mediator;
using Marketplace.Queries.Contracts.ClassifiedAds.QueryFilters;
using Marketplace.Queries.Contracts.ClassifiedAds.QueryResults;
using Raven.Client.Documents.Session;

namespace Marketplace.Queries.RavenDB.ClassifiedAds.QueryHandlers
{
	internal class GetPublicClassifiedAdQueryHandler : IQueryHandler<GetPublicClassifiedAdQueryFilter, List<ClassifiedAdItem>>
    {
		public GetPublicClassifiedAdQueryHandler()
		{
		}

		public Task<List<ClassifiedAdItem>> Handle(GetPublicClassifiedAdQueryFilter request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
