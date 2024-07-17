using Framework.Application.Mediator;
using Framework.Application.Streaming;
using Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;
using Marketplace.Domain.ClassifiedAds;
using Marketplace.Domain.ClassifiedAds.ValueObjects;
using Marketplace.Domain.Shared.ValueObjects;

namespace Marketplace.Application.ClassifiedAds.CommandHandlers;

internal class UpdateClassifiedAdTextCommandHandler : ICommandHandler<UpdateClassifiedAdTextCommand>
{
	private readonly IAggregateStore _aggregateStore;
	public UpdateClassifiedAdTextCommandHandler(IAggregateStore aggregateStore)
	{
		_aggregateStore = aggregateStore;
	}

	public async Task Handle(UpdateClassifiedAdTextCommand request, CancellationToken cancellationToken)
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

		classifiedAd.UpdateText(ClassifiedAdText.FromString(request.Text));

		await _aggregateStore.Save<ClassifiedAd, ClassifiedAdId>(classifiedAd, cancellationToken);
	}
}
