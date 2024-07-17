using Framework.Application.Mediator;
using Framework.Application.Streaming;
using Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;
using Marketplace.Domain.ClassifiedAds;
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
		var fetchClassifiedAd = await _aggregateStore.Load<ClassifiedAd, ClassifiedAdId>(new ClassifiedAdId(request.Id));

		if (fetchClassifiedAd != null)
		{
			if (fetchClassifiedAd.IsDeleted == true)
			{
				throw new InvalidOperationException($"Entity with id {request.Id} has been removed");
			}

			throw new InvalidOperationException($"Entity with id {request.Id} already exists");
		}

		var classifiedAd = new ClassifiedAd(new ClassifiedAdId(request.Id), new UserProfileId(request.OwnerId));

		await _aggregateStore.Save(classifiedAd, cancellationToken);

		return request.Id;
	}
}
