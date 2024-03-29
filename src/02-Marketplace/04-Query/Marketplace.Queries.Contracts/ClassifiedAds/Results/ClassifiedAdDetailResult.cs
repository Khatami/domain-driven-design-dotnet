﻿namespace Marketplace.Queries.Contracts.ClassifiedAds.Results
{
	public class ClassifiedAdDetailResult
	{
		public Guid ClassifiedAdId { get; set; }
		public Guid SellerId { get; set; }
		public string Title { get; set; }
		public decimal Price { get; set; }
		public string CurrencyCode { get; set; }
		public string Description { get; set; }
		public string SellersDisplayName { get; set; }
		public string[] PhotoUrls { get; set; }
	}
}