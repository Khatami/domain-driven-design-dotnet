using Marketplace.Domain.ClassifiedAds;
using Marketplace.Domain.ClassifiedAds.Arguments;
using Marketplace.Domain.ClassifiedAds.Enums;
using Marketplace.Domain.ClassifiedAds.Exceptions;
using Marketplace.Domain.ClassifiedAds.ValueObjects;
using Marketplace.Domain.Tests.ClassifiedAds.FakeServices;

namespace Marketplace.Domain.Tests.ClassifiedAds
{
	public class ClassifiedAd_Publish_Spec
	{
		private readonly ClassifiedAd _classifiedAd;
		public ClassifiedAd_Publish_Spec()
		{
			_classifiedAd = new ClassifiedAd(new ClassifiedAdId(Guid.NewGuid()), new UserId(Guid.NewGuid()));
		}

		[Fact]
		public void Can_publish_a_valid_ad()
		{
			_classifiedAd.SetTitle(ClassifiedAdTitle.FromString("Test Ad"));
			_classifiedAd.UpdateText(ClassifiedAdText.FromString("Please buy my stuff"));
			_classifiedAd.UpdatePrice(Price.FromDecimal(new MoneyArguments(100.10m, "EUR", new FakeCurrencyLookup())));

			_classifiedAd.AddPicture(new Uri("https://cdn.marketplace.com"), new PictureSize(900, 1000));

			_classifiedAd.RequestToPublish(Guid.NewGuid());

			Assert.Equal(ClassifiedAdState.PendingReview, _classifiedAd.State);
		}

		[Fact]
		public void Cannot_publish_without_title()
		{
			_classifiedAd.UpdateText(ClassifiedAdText.FromString("Please buy my stuff"));
			_classifiedAd.UpdatePrice(Price.FromDecimal(new MoneyArguments(100.10m, "EUR", new FakeCurrencyLookup())));

			Assert.Throws<InvalidEntityStateException>(() => _classifiedAd.RequestToPublish(Guid.NewGuid()));
		}

		[Fact]
		public void Cannot_publish_without_text()
		{
			_classifiedAd.SetTitle(ClassifiedAdTitle.FromString("Test Ad"));
			_classifiedAd.UpdatePrice(Price.FromDecimal(new MoneyArguments(100.10m, "EUR", new FakeCurrencyLookup())));

			Assert.Throws<InvalidEntityStateException>(() => _classifiedAd.RequestToPublish(Guid.NewGuid()));
		}

		[Fact]
		public void Cannot_publish_without_price()
		{
			_classifiedAd.SetTitle(ClassifiedAdTitle.FromString("Test Ad"));
			_classifiedAd.UpdateText(ClassifiedAdText.FromString("Please buy my stuff"));

			Assert.Throws<InvalidEntityStateException>(() => _classifiedAd.RequestToPublish(Guid.NewGuid()));
		}

		[Fact]
		public void Cannot_publish_with_zero_price()
		{
			_classifiedAd.SetTitle(ClassifiedAdTitle.FromString("Test Ad"));
			_classifiedAd.UpdateText(ClassifiedAdText.FromString("Please buy my stuff"));
			_classifiedAd.UpdatePrice(Price.FromDecimal(new MoneyArguments(0, "EUR", new FakeCurrencyLookup())));

			Assert.Throws<InvalidEntityStateException>(() => _classifiedAd.RequestToPublish(Guid.NewGuid()));
		}

		[Fact]
		public void Cannot_publish_without_picture()
		{
			_classifiedAd.SetTitle(ClassifiedAdTitle.FromString("Test Ad"));
			_classifiedAd.UpdateText(ClassifiedAdText.FromString("Please buy my stuff"));
			_classifiedAd.UpdatePrice(Price.FromDecimal(new MoneyArguments(12, "EUR", new FakeCurrencyLookup())));

			Assert.Throws<InvalidEntityStateException>(() => _classifiedAd.RequestToPublish(Guid.NewGuid()));
		}
	}
}
