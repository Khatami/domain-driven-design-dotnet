using Marketplace.Domain.Shared.ValueObjects;

namespace Marketplace.Domain.ClassifiedAds
{
    public interface IClassifiedAdRepository
	{
		Task AddAsync(ClassifiedAd entity);

		Task<bool> ExistsAsync(ClassifiedAdId id);

		Task<ClassifiedAd> GetAsync(ClassifiedAdId id);
	}
}