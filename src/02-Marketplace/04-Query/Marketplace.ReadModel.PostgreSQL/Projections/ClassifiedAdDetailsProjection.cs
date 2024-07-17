using Framework.Query.Attributes;
using Framework.Query.Streaming;
using Marketplace.Domain.Events.ClassifiedAds;
using Marketplace.Domain.Events.UserProfiles;
using Marketplace.ReadModel.PostgreSQL.Exceptions;
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
					var exists = _databaseContext.ClassifiedAdDetails.Any(x => x.ClassifiedAdId == e.Id);

					if (exists == false)
					{
						_databaseContext.ClassifiedAdDetails.Add(new ClassifiedAdDetail
						{
							ClassifiedAdId = e.Id,
							SellerId = e.OwnerId,
							Version = version
						});
					}
					break;
				case ClassifiedAdTitleChanged e:
					UpdateItem(e.Id, ad =>
					{
						ad.Title = e.Title;
					}, version);
					break;
				case ClassifiedAdTextChanged e:
					UpdateItem(e.Id, ad =>
					{
						ad.Description = e.AdText;
					}, version);
					break;
				case ClassifiedAdPriceUpdated e:
					UpdateItem(e.Id, ad =>
					{
						ad.Price = e.Price;
						ad.CurrencyCode = e.CurrencyCode;
					}, version);
					break;
				case UserDisplayNameUpdated e:
					UpdateMultipleItems(x => x.SellerId == e.UserId,
						x =>
						{
							x.SellersDisplayName = e.DisplayName;
						}, version);
					break;
				case ClassifiedAdRemoved e:
					UpdateItem(e.Id, ad =>
					{
						ad.IsDeleted = true;
					}, version);
					break;
				default:
					throw new NotImplementedException($"the following event is not implemented: {@event.ToString()}");
			}

			await _databaseContext.SaveChangesAsync();
		}

		private void UpdateItem(Guid id, Action<ClassifiedAdDetail> update, long version)
		{
			var item = _databaseContext.ClassifiedAdDetails.FirstOrDefault(x => x.ClassifiedAdId == id);

			if (item == null)
			{
				throw new ArgumentNullException(nameof(item));
			}

			if (version <= item.Version)
			{
				return;
			}

			if (item.Version + 1 != version)
			{
				throw new ConcurrencyMismatchException();
			}

			item.Version = version;

			update(item);
		}

		private void UpdateMultipleItems(Func<ClassifiedAdDetail, bool> query,
			Action<ClassifiedAdDetail> update, 
			long version)
		{
			foreach (var item in _databaseContext.ClassifiedAdDetails.Where(query))
			{
				UpdateItem(item.ClassifiedAdId, update, version);
			}
		}
	}
}
