using Marketplace.Application.Infrastructure.Mediator;
using Marketplace.Queries.Contracts.ClassifiedAds.QueryFilters;
using Marketplace.Queries.Contracts.ClassifiedAds.QueryResults;
using Raven.Client.Documents.Session;

namespace Marketplace.Queries.RavenDB.ClassifiedAds.QueryHandlers
{
	internal class ClassifiedAdListItemQueryHandler : IQueryHandler<GetPublishedAdsQueryFilter, List<ClassifiedAdItem>>
    {
        private readonly IAsyncDocumentSession _session;

		public ClassifiedAdListItemQueryHandler(IAsyncDocumentSession session)
		{
			_session = session;
		}

		public async Task<List<ClassifiedAdItem>> Handle(GetPublishedAdsQueryFilter request, CancellationToken cancellationToken)
        {
            return new List<ClassifiedAdItem>();
        }
    }
}
