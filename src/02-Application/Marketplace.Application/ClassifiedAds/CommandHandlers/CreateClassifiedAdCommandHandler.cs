using Marketplace.Domain.ClassifiedAds;
using Marketplace.Application.Infrastructure;
using Marketplace.Domain.Shared.ValueObjects;
using Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;

namespace Marketplace.Application.ClassifiedAds.CommandHandlers;

internal class CreateClassifiedAdCommandHandler : Mediator.ICommandHandler<CreateClassifiedAdCommand>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IClassifiedAdRepository _classifiedAdRepository;

	public CreateClassifiedAdCommandHandler(IUnitOfWork unitOfWork, IClassifiedAdRepository classifiedAdRepository)
	{
		_unitOfWork = unitOfWork;
		_classifiedAdRepository = classifiedAdRepository;
	}


	public async Task Handle(CreateClassifiedAdCommand request, CancellationToken cancellationToken)
	{
		var exists = await _classifiedAdRepository.Exists(new ClassifiedAdId(request.Id));

		if (exists)
		{
			throw new InvalidOperationException($"Entity with id {request.Id} already exists");
		}

		var classifiedAd = new ClassifiedAd(new ClassifiedAdId(request.Id), new UserId(request.OwnerId));

		await _classifiedAdRepository.Add(classifiedAd);

		await _unitOfWork.Commit();
	}
}
