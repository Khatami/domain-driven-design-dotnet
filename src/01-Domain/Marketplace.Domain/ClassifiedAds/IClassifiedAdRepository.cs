using Marketplace.Domain.Shared.ValueObjects;

namespace Marketplace.Domain.ClassifiedAds
{
    public interface IClassifiedAdRepository
	{
		Task Add(ClassifiedAd entity);

		Task<bool> Exists(ClassifiedAdId id);

		Task<ClassifiedAd> Load(ClassifiedAdId id);
	}
}