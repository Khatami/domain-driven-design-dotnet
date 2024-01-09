using Marketplace.Application.SeedWork.Mediator;
using Marketplace.Queries.Contracts.ReadModels.ClassifiedAds;

namespace Marketplace.Queries.Contracts.Queries.ClassifiedAds
{
    public class GetPublicClassifiedAdQueryFilter : IQuery<List<ClassifiedAdItem>>
    {
        public Guid ClassifiedAdId { get; set; }
    }
}
