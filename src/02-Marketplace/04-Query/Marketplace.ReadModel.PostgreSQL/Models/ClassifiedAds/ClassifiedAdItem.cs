using System.ComponentModel.DataAnnotations;

namespace Marketplace.ReadModel.PostgreSQL.Models.ClassifiedAds
{
	public class ClassifiedAdItem
	{
		[Key]
		public Guid ClassifiedAdId { get; set; }

		public string? Title { get; set; }

		public decimal? Price { get; set; }

		public string? CurrencyCode { get; set; }

		public string? PhotoUrl { get; set; }

		[ConcurrencyCheck]
		public long Version { get; set; }
	}
}