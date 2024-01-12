using Framework.Application.Mediator;
using Framework.Application.Streaming;
using Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;
using Marketplace.Domain.ClassifiedAds;
using Marketplace.Domain.ClassifiedAds.Arguments;
using Marketplace.Domain.ClassifiedAds.DomainServices;
using Marketplace.Domain.ClassifiedAds.ValueObjects;
using Marketplace.Domain.Shared.ValueObjects;

namespace Marketplace.Application.ClassifiedAds.CommandHandlers;

internal class UpdateClassifiedAdPriceCommandHandler : ICommandHandler<UpdateClassifiedAdPriceCommand>
{
	private readonly IAggregateStore _aggregateStore;
	private readonly ICurrencyLookup _currencyLookup;

	public UpdateClassifiedAdPriceCommandHandler(IAggregateStore aggregateStore, ICurrencyLookup currencyLookup)
	{
		_aggregateStore = aggregateStore;
		_currencyLookup = currencyLookup;
	}

	public async Task Handle(UpdateClassifiedAdPriceCommand request, CancellationToken cancellationToken)
	{
		var classifiedAd = await _aggregateStore.Load<ClassifiedAd, ClassifiedAdId>(new ClassifiedAdId(request.Id));

		if (classifiedAd == null)
		{
			throw new InvalidOperationException($"Entity with id {request.Id} cannot be found");
		}

		classifiedAd.UpdatePrice(Price.FromDecimal(new MoneyArguments(request.Price, request.Currency, _currencyLookup)));

		await _aggregateStore.Save<ClassifiedAd, ClassifiedAdId>(classifiedAd, cancellationToken);
	}
}
