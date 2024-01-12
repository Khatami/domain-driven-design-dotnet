using Framework.Domain.Aggregation;

namespace Framework.Application.Streaming
{
	public interface IAggregateStore
	{
		Task<bool> Exists<T, TId>(TId aggregateId);
		Task<T> Load<T, TId>(TId aggregateId) where T : AggregateRoot<TId>;
		Task Save<T, TId>(T aggregate, CancellationToken cancellationToken) where T : AggregateRoot<TId>;
		Task Save<TId>(AggregateRoot<TId> aggregate, CancellationToken cancellationToken = default);
		Task Save(AggregateRootBase aggregate, CancellationToken cancellationToken = default);
		Task Save(string streamName, long version, IEnumerable<object> events);
		string GetStreamName<T, TId>(TId aggregateId);
		string GetStreamName<T, TId>(T aggregate) where T : AggregateRoot<TId>;
		string GetStreamName<TId>(AggregateRoot<TId> aggregateRoot);
		string GetStreamName(AggregateRootBase aggregateRoot);
	}
}
