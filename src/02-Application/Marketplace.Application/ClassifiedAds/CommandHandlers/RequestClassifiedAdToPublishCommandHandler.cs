using Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;
using Marketplace.Application.Infrastructure.Mediator;
using Marketplace.Domain.ClassifiedAds;
using Marketplace.Domain.SeedWork.Streaming;
using Marketplace.Domain.Shared.ValueObjects;

namespace Marketplace.Application.ClassifiedAds.CommandHandlers;

internal class RequestClassifiedAdToPublishCommandHandler : ICommandHandler<RequestClassifiedAdToPublishCommand>
{
	private readonly IAggregateStore _aggregateStore;
	public RequestClassifiedAdToPublishCommandHandler(IAggregateStore aggregateStore)
	{
		_aggregateStore = aggregateStore;
	}

	public async Task Handle(RequestClassifiedAdToPublishCommand request, CancellationToken cancellationToken)
	{
		var classifiedAd = await _aggregateStore.Load<ClassifiedAd, ClassifiedAdId>(new ClassifiedAdId(request.Id));

		if (classifiedAd == null)
		{
			throw new InvalidOperationException($"Entity with id {request.Id} cannot be found");
		}

		classifiedAd.RequestToPublish(Guid.NewGuid());

		await _aggregateStore.Save<ClassifiedAd, ClassifiedAdId>(classifiedAd);
	}
}
