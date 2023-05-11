using Marketplace.Domain.ClassifiedAds.ValueObjects;
using Marketplace.Domain.Helpers;

namespace Marketplace.Domain.ClassifiedAds
{
	public interface IClassifiedAdRepository
	{
		Task Add(ClassifiedAd entity);

		Task<bool> Exists(ClassifiedAdId id);

		Task<ClassifiedAd?> Load(ClassifiedAdId id);
	}
}