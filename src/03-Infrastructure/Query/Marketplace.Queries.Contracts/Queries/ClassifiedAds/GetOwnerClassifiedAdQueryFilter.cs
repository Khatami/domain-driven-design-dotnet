using Marketplace.Application.SeedWork.Mediator;
using Marketplace.Queries.Contracts.ReadModels.ClassifiedAds;

namespace Marketplace.Queries.Contracts.Queries.ClassifiedAds
{
    public class GetOwnerClassifiedAdQueryFilter : IQuery<List<ClassifiedAdItem>>
    {
        public Guid OwnerId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
