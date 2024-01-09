﻿using Marketplace.Domain.Events.ClassifiedAds;
using Marketplace.Domain.Events.UserProfiles;
using Marketplace.Infrastructure.Subscribtions.Infrastructure;
using Marketplace.Queries.Contracts.ReadModels.ClassifiedAds;

namespace Marketplace.Infrastructure.Subscribtions.Projections
{
    public class ClassifiedAdDetailsProjection : IProjection
    {
        public static List<ClassifiedAdDetail> ClassifiedAdDetails = new();

        public Task Project(object @event)
        {
            switch (@event)
            {
                case ClassifiedAdCreated e:
                    ClassifiedAdDetails.Add(new ClassifiedAdDetail
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

            return Task.CompletedTask;
        }

        private void UpdateItem(Guid id, Action<ClassifiedAdDetail> update)
        {
            var item = ClassifiedAdDetails.FirstOrDefault(x => x.ClassifiedAdId == id);

            if (item == null) return;

            update(item);
        }

        private void UpdateMultipleItems(Func<ClassifiedAdDetail, bool> query,
            Action<ClassifiedAdDetail> update)
        {
            foreach (var item in ClassifiedAdDetails.Where(query))
            {
                update(item);
            }
        }
    }
}
