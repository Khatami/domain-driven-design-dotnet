﻿namespace Marketplace.Application.Contracts.ClassifiedAds.Queries.QueryModels;

public class GetPublishedClassifiedAdsQuery
{
	public int Page { get; set; }
	public int PageSize { get; set; }
}
