using Marketplace.Application.SeedWork.Mediator;
using Marketplace.Queries.Contracts.Queries.ClassifiedAds;
using Marketplace.Queries.Contracts.ReadModels.ClassifiedAds;

namespace Marketplace.Queries.EF.ClassifiedAds.QueryHandlers
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
