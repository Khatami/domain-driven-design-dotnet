using Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;
using Marketplace.Application.SeedWork.Mediator;
using Marketplace.Domain.ClassifiedAds;
using Marketplace.Domain.SeedWork.Comparison;
using Marketplace.Domain.SeedWork.Streaming;
using Marketplace.Domain.Shared.ValueObjects;

namespace Marketplace.Application.ClassifiedAds.CommandHandlers;

internal class SnapshotClassifiedAdCommandHandler : ICommandHandler<SnapshotClassifiedAdCommand>
{
	private readonly IAggregateStore _aggregateStore;
	private readonly IComparisonService _comparisonService;
	public SnapshotClassifiedAdCommandHandler(IAggregateStore aggregateStore, IComparisonService comparisonService)
	{
		_aggregateStore = aggregateStore;
		_comparisonService = comparisonService;
	}

	public async Task Handle(SnapshotClassifiedAdCommand request, CancellationToken cancellationToken)
	{
		var classifiedAd = await _aggregateStore.Load<ClassifiedAd, ClassifiedAdId>(new ClassifiedAdId(request.Id));

		if (classifiedAd == null)
		{
			throw new InvalidOperationException($"Entity with id {request.Id} cannot be found");
		}

		classifiedAd.Snapshot(_comparisonService);

		await _aggregateStore.Save(classifiedAd, cancellationToken);
	}
}
