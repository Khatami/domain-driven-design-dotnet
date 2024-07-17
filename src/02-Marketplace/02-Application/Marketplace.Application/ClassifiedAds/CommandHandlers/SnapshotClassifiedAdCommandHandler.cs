using Framework.Application.Mediator;
using Framework.Application.Streaming;
using Framework.Domain.Comparison;
using Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;
using Marketplace.Domain.ClassifiedAds;
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
