using Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;
using Marketplace.Application.SeedWork.Mediator;
using Marketplace.Domain.ClassifiedAds;
using Marketplace.Domain.SeedWork.Streaming;
using Marketplace.Domain.Shared.ValueObjects;

namespace Marketplace.Application.ClassifiedAds.CommandHandlers;

internal class SnapshotClassifiedAdCommandHandler : ICommandHandler<SnapshotClassifiedAdCommand>
{
	private readonly IAggregateStore _aggregateStore;
	public SnapshotClassifiedAdCommandHandler(IAggregateStore aggregateStore)
	{
		_aggregateStore = aggregateStore;
	}

	public async Task Handle(SnapshotClassifiedAdCommand request, CancellationToken cancellationToken)
	{
		var classifiedAd = await _aggregateStore.Load<ClassifiedAd, ClassifiedAdId>(new ClassifiedAdId(request.Id));

		if (classifiedAd == null)
		{
			throw new InvalidOperationException($"Entity with id {request.Id} cannot be found");
		}

		classifiedAd.Snapshot();

		await _aggregateStore.Save(classifiedAd, cancellationToken);
	}
}
