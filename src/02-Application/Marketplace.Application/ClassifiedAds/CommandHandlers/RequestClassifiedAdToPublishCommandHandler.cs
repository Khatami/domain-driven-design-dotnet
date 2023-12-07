using Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;
using Marketplace.Application.Infrastructure;
using Marketplace.Application.Infrastructure.Mediator;
using Marketplace.Domain.ClassifiedAds;
using Marketplace.Domain.Shared.ValueObjects;

namespace Marketplace.Application.ClassifiedAds.CommandHandlers;

internal class RequestClassifiedAdToPublishCommandHandler : ICommandHandler<RequestClassifiedAdToPublishCommand>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IClassifiedAdRepository _classifiedAdRepository;

	public RequestClassifiedAdToPublishCommandHandler(IUnitOfWork unitOfWork, IClassifiedAdRepository classifiedAdRepository)
	{
		_unitOfWork = unitOfWork;
		_classifiedAdRepository = classifiedAdRepository;
	}


	public async Task Handle(RequestClassifiedAdToPublishCommand request, CancellationToken cancellationToken)
	{
		var classifiedAd = await _classifiedAdRepository.Load(new ClassifiedAdId(request.Id));

		if (classifiedAd == null)
		{
			throw new InvalidOperationException($"Entity with id {request.Id} cannot be found");
		}

		classifiedAd.RequestToPublish(Guid.NewGuid());

		await _unitOfWork.Commit();
	}
}
