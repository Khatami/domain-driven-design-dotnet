﻿using Framework.Application.Mediator;
using Framework.Application.Streaming;
using Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;
using Marketplace.Domain.ClassifiedAds;
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

		if (classifiedAd.IsDeleted == true)
		{
			throw new InvalidOperationException($"Entity with id {request.Id} has been removed");
		}

		classifiedAd.RequestToPublish(Guid.NewGuid());

		await _aggregateStore.Save<ClassifiedAd, ClassifiedAdId>(classifiedAd, cancellationToken);
	}
}
