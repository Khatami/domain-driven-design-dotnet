using Marketplace.Domain.ClassifiedAds.DomainServices;
using Marketplace.Domain.ClassifiedAds.Metadata;

namespace Marketplace.Domain.Tests.ClassifiedAds.FakeServices
{
	public class FakeCurrencyLookup : ICurrencyLookup
	{
		private static readonly IEnumerable<Currency> _currencies = new[]
		{
			new Currency()
			{
				CurrencyCode = "EUR",
				DecimalPoints = 2,
				IsUse = true
			},
			new Currency()
			{
				CurrencyCode = "USD",
				DecimalPoints = 2,
				IsUse = true
			},
			new Currency()
			{
				CurrencyCode = "JPY",
				DecimalPoints = 2,
				IsUse = true
			},
			new Currency()
			{
				CurrencyCode = "DEM",
				DecimalPoints = 2,
				IsUse = false
			}
		};

		public Currency FindCurrency(string currencyCode)
		{
			var currency = _currencies.FirstOrDefault(q => q.CurrencyCode == currencyCode);

			return currency ?? Currency.None;
		}
	}
}
