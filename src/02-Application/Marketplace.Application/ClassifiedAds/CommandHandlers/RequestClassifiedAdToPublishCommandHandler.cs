using Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;
using Marketplace.Application.Infrastructure;
using Marketplace.Domain.ClassifiedAds.DomainServices;
using Marketplace.Domain.ClassifiedAds;
using Marketplace.Domain.ClassifiedAds.ValueObjects;
using Marketplace.Domain.Shared.ValueObjects;

namespace Marketplace.Application.ClassifiedAds.CommandHandlers;

internal class RequestClassifiedAdToPublishCommandHandler : Mediator.ICommandHandler<RequestClassifiedAdToPublishCommand>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICurrencyLookup _currencyLookup;
	private readonly IClassifiedAdRepository _classifiedAdRepository;

	public RequestClassifiedAdToPublishCommandHandler
		(IUnitOfWork unitOfWork, ICurrencyLookup currencyLookup,
		IClassifiedAdRepository classifiedAdRepository)
	{
		_unitOfWork = unitOfWork;
		_currencyLookup = currencyLookup;
		_classifiedAdRepository = classifiedAdRepository;
	}


	public async Task Handle(RequestClassifiedAdToPublishCommand request, CancellationToken cancellationToken)
	{
		var classifiedAd = await _classifiedAdRepository.Load(new ClassifiedAdId(request.Id));

		if (classifiedAd == null)
			throw new InvalidOperationException($"Entity with id {request.Id} cannot be found");

		classifiedAd.RequestToPublish(Guid.NewGuid());

		await _unitOfWork.Commit();
		break;
	}
}
