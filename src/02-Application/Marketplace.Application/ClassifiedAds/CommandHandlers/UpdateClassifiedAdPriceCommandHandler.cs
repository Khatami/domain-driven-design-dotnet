using Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;
using Marketplace.Application.Infrastructure;
using Marketplace.Domain.ClassifiedAds.Arguments;
using Marketplace.Domain.ClassifiedAds.DomainServices;
using Marketplace.Domain.ClassifiedAds.ValueObjects;
using Marketplace.Domain.ClassifiedAds;
using Marketplace.Domain.Shared.ValueObjects;
namespace Marketplace.Application.ClassifiedAds.CommandHandlers;

internal class UpdateClassifiedAdPriceCommandHandler : Mediator.ICommandHandler<UpdateClassifiedAdPriceCommand>
{
	public Task Handle(UpdateClassifiedAdPriceCommand request, CancellationToken cancellationToken)
	{
		classifiedAd = await _classifiedAdRepository.Load(new ClassifiedAdId(cmd.Id));

		if (classifiedAd == null)
			throw new InvalidOperationException($"Entity with id {cmd.Id} cannot be found");

		classifiedAd.UpdatePrice(Price.FromDecimal(new MoneyArguments(cmd.Price, cmd.Currency, _currencyLookup)));

		await _unitOfWork.Commit();
		break;

	}
}
