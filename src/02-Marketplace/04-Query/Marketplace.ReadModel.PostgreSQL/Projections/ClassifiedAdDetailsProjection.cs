using Framework.Query.Attributes;
using Framework.Query.Streaming;
using Marketplace.Domain.Events.ClassifiedAds;
using Marketplace.Domain.Events.UserProfiles;
using Marketplace.ReadModel.PostgreSQL.Models.ClassifiedAds;

namespace Marketplace.ReadModel.PostgreSQL.Projections
{
	[Streaming(Stream.ClassifiedAd)]
	public class ClassifiedAdDetailsProjection : IProjection
	{
		private readonly MarketplaceReadModelDbContext _databaseContext;

		public ClassifiedAdDetailsProjection(MarketplaceReadModelDbContext databaseContext)
		{
			_databaseContext = databaseContext;
		}

		public async Task Project(object @event, string stream, long eventNumberInStream, long version)
		{
			switch (@event)
			{
				case ClassifiedAdCreated e:
					_databaseContext.ClassifiedAdDetails.Add(new ClassifiedAdDetail
					{
						ClassifiedAdId = e.Id,
						SellerId = e.OwnerId
					});
					break;
				case ClassifiedAdTitleChanged e:
					UpdateItem(e.Id, ad => ad.Title = e.Title);
					break;
				case ClassifiedAdTextChanged e:
					UpdateItem(e.Id, ad => ad.Description = e.AdText);
					break;
				case ClassifiedAdPriceUpdated e:
					UpdateItem(e.Id, ad =>
					{
						ad.Price = e.Price;
						ad.CurrencyCode = e.CurrencyCode;
					});
					break;
				case UserDisplayNameUpdated e:
					UpdateMultipleItems(x => x.SellerId == e.UserId,
						 x => x.SellersDisplayName = e.DisplayName);
					break;
			}

			await _databaseContext.SaveChangesAsync();
		}

		private void UpdateItem(Guid id, Action<ClassifiedAdDetail> update)
		{
			var item = _databaseContext.ClassifiedAdDetails.FirstOrDefault(x => x.ClassifiedAdId == id);

			if (item == null) return;

			update(item);
		}

		private void UpdateMultipleItems(Func<ClassifiedAdDetail, bool> query,
			Action<ClassifiedAdDetail> update)
		{
			foreach (var item in _databaseContext.ClassifiedAdDetails.Where(query))
			{
				update(item);
			}
		}
	}
}
