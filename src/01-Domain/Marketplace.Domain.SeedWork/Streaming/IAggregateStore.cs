using Marketplace.Domain.SeedWork.Aggregation;

namespace Marketplace.Domain.SeedWork.Streaming
{
	public interface IAggregateStore
	{
		Task<bool> Exists<T, TId>(TId aggregateId);
		Task Save<T, TId>(T aggregate, CancellationToken cancellationToken) where T : AggregateRoot<TId>;
		Task Save<TId>(AggregateRoot<TId> aggregate, CancellationToken cancellationToken);
		Task Save(AggregateRootBase aggregate, CancellationToken cancellationToken);
		Task<T> Load<T, TId>(TId aggregateId) where T : AggregateRoot<TId>;
	}
}
