﻿using Marketplace.Application.SeedWork.Mediator;
using Marketplace.Queries.Contracts.Queries.ClassifiedAds;
using Marketplace.Queries.Contracts.ReadModels.ClassifiedAds;

namespace Marketplace.Queries.EF.ClassifiedAds.QueryHandlers
{
    internal class GetPublishedAdsQueryHandler : IQueryHandler<GetPublishedAdsQueryFilter, List<ClassifiedAdItem>>
	{
		public GetPublishedAdsQueryHandler()
		{
		}

		public async Task<List<ClassifiedAdItem>> Handle(GetPublishedAdsQueryFilter request, CancellationToken cancellationToken)
		{
			return new List<ClassifiedAdItem>();
		}
	}
}
