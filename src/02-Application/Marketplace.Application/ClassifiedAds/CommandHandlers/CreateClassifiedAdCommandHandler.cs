using Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;
using Marketplace.Application.Infrastructure.Mediator;
using Marketplace.Domain.ClassifiedAds;
using Marketplace.Domain.SeedWork.Streaming;
using Marketplace.Domain.Shared.ValueObjects;

namespace Marketplace.Application.ClassifiedAds.CommandHandlers;

internal class CreateClassifiedAdCommandHandler : ICommandHandler<CreateClassifiedAdCommand, Guid>
{
	private readonly IAggregateStore _aggregateStore;

	public CreateClassifiedAdCommandHandler(IAggregateStore aggregateStore)
	{
		_aggregateStore = aggregateStore;
	}

	public async Task<Guid> Handle(CreateClassifiedAdCommand request, CancellationToken cancellationToken)
	{
		var exists = await _aggregateStore.Exists<ClassifiedAd, ClassifiedAdId>(new ClassifiedAdId(request.Id));

		if (exists)
		{
			throw new InvalidOperationException($"Entity with id {request.Id} already exists");
		}

		var classifiedAd = new ClassifiedAd(new ClassifiedAdId(request.Id), new UserProfileId(request.OwnerId));

		await _aggregateStore.Save(classifiedAd);

		return request.Id;
	}
}
