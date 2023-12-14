using Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;
using Marketplace.Application.Infrastructure.Mediator;
using Marketplace.Application.Infrastructure.UnitOfWork;
using Marketplace.Domain.ClassifiedAds;
using Marketplace.Domain.ClassifiedAds.Arguments;
using Marketplace.Domain.ClassifiedAds.DomainServices;
using Marketplace.Domain.ClassifiedAds.ValueObjects;
using Marketplace.Domain.Shared.ValueObjects;

namespace Marketplace.Application.ClassifiedAds.CommandHandlers;

internal class UpdateClassifiedAdPriceCommandHandler : ICommandHandler<UpdateClassifiedAdPriceCommand>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICurrencyLookup _currencyLookup;
	private readonly IClassifiedAdRepository _classifiedAdRepository;

	public UpdateClassifiedAdPriceCommandHandler
		(IUnitOfWork unitOfWork, ICurrencyLookup currencyLookup, IClassifiedAdRepository classifiedAdRepository)
	{
		_unitOfWork = unitOfWork;
		_currencyLookup = currencyLookup;
		_classifiedAdRepository = classifiedAdRepository;
	}

	public async Task Handle(UpdateClassifiedAdPriceCommand request, CancellationToken cancellationToken)
	{
		var classifiedAd = await _classifiedAdRepository.GetAsync(new ClassifiedAdId(request.Id));

		if (classifiedAd == null)
		{
			throw new InvalidOperationException($"Entity with id {request.Id} cannot be found");
		}

		classifiedAd.UpdatePrice(Price.FromDecimal(new MoneyArguments(request.Price, request.Currency, _currencyLookup)));

		await _unitOfWork.Commit();
	}
}
