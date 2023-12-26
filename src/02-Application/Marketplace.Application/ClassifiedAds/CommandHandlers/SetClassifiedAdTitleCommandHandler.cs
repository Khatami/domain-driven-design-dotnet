using Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;
using Marketplace.Application.SeedWork.Mediator;
using Marketplace.Domain.ClassifiedAds;
using Marketplace.Domain.ClassifiedAds.ValueObjects;
using Marketplace.Domain.SeedWork.Streaming;
using Marketplace.Domain.Shared.ValueObjects;

namespace Marketplace.Application.ClassifiedAds.CommandHandlers;

internal class SetClassifiedAdTitleCommandHandler : ICommandHandler<SetClassifiedAdTitleCommand>
{
	private readonly IAggregateStore _aggregateStore;
	public SetClassifiedAdTitleCommandHandler(IAggregateStore aggregateStore)
	{
		_aggregateStore = aggregateStore;
	}

	public async Task Handle(SetClassifiedAdTitleCommand request, CancellationToken cancellationToken)
	{
		var classifiedAd = await _aggregateStore.Load<ClassifiedAd, ClassifiedAdId>(new ClassifiedAdId(request.Id));

		if (classifiedAd == null)
		{
			throw new InvalidOperationException($"Entity with id {request.Id} cannot be found");
		}

		classifiedAd.SetTitle(ClassifiedAdTitle.FromString(request.Title));

		await _aggregateStore.Save<ClassifiedAd, ClassifiedAdId>(classifiedAd, cancellationToken);
	}
}
