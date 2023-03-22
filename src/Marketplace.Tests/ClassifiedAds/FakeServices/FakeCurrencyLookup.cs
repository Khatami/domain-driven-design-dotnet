using Marketplace.Domain.ClassifiedAds.DomainServices;
using Marketplace.Domain.ClassifiedAds.Metadata;

namespace Marketplace.Tests.ClassifiedAds.FakeServices
{
	public class FakeCurrencyLookup : ICurrencyLookup
	{
		private static readonly IEnumerable<CurrencyDetails> _currencies = new[]
		{
			new CurrencyDetails()
			{
				CurrencyCode = "EUR",
				DecimalPoints = 2,
				IsUse = true
			},
			new CurrencyDetails()
			{
				CurrencyCode = "USD",
				DecimalPoints = 2,
				IsUse = true
			},
			new CurrencyDetails()
			{
				CurrencyCode = "JPY",
				DecimalPoints = 2,
				IsUse = true
			},
			new CurrencyDetails()
			{
				CurrencyCode = "DEM",
				DecimalPoints = 2,
				IsUse = false
			}
		};

		public CurrencyDetails FindCurrency(string currencyCode)
		{
			var currency = _currencies.FirstOrDefault(q => q.CurrencyCode == currencyCode);

			return currency ?? CurrencyDetails.None;
		}
	}
}
