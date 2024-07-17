using Framework.Query.Mediator;
using Marketplace.Queries.Contracts.ClassifiedAds.Filters;
using Marketplace.Queries.Contracts.ClassifiedAds.Results;
using Marketplace.ReadModel.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Queries.ClassifiedAds
{
	internal class GetOwnerClassifiedAdQueryHandler : IQueryHandler<GetOwnerClassifiedAdQueryFilter, List<ClassifiedAdItemResult>>
	{
		private readonly MarketplaceReadModelDbContext _databaseContext;

		public GetOwnerClassifiedAdQueryHandler(MarketplaceReadModelDbContext databaseContext)
		{
			_databaseContext = databaseContext;
		}

		public async Task<List<ClassifiedAdItemResult>> Handle(GetOwnerClassifiedAdQueryFilter request, CancellationToken cancellationToken)
		{
			return await _databaseContext.ClassifiedAdDetails
				.Where(current => current.IsDeleted == false)
				.Where(current => current.SellerId == request.OwnerId)
				.OrderBy(current => current.ClassifiedAdId)
				.Skip((request.Page - 1) * request.PageSize)
				.Take(request.PageSize)
				.Select(current => new ClassifiedAdItemResult()
				{
					ClassifiedAdId = current.ClassifiedAdId,
					CurrencyCode = current.CurrencyCode ?? string.Empty,
					PhotoUrl = current.PhotoUrls != null ? current.PhotoUrls.FirstOrDefault() ?? string.Empty : string.Empty,
					Price = current.Price ?? 0,
					Title = current.Title ?? string.Empty
				})
				.ToListAsync();
		}
	}
}
