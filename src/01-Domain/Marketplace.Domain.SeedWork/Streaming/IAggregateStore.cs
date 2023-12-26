using Marketplace.Domain.SeedWork.Aggregation;

namespace Marketplace.Domain.SeedWork.Streaming
{
	public interface IAggregateStore
	{
		Task<bool> Exists<T, TId>(TId aggregateId);
		Task Save<T, TId>(T aggregate) where T : AggregateRoot<TId>;
		Task Save<TId>(AggregateRoot<TId> aggregate);
		Task Save(AggregateRootBase aggregate);
		Task<T> Load<T, TId>(TId aggregateId) where T : AggregateRoot<TId>;
	}
}
