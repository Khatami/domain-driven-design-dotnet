using Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;
using Marketplace.Application.Infrastructure.Mediator;
using Marketplace.Application.Infrastructure.UnitOfWork;
using Marketplace.Domain.ClassifiedAds;
using Marketplace.Domain.Shared.ValueObjects;

namespace Marketplace.Application.ClassifiedAds.CommandHandlers;

internal class CreateClassifiedAdCommandHandler : ICommandHandler<CreateClassifiedAdCommand, Guid>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IClassifiedAdRepository _classifiedAdRepository;

	public CreateClassifiedAdCommandHandler(IUnitOfWork unitOfWork, IClassifiedAdRepository classifiedAdRepository)
	{
		_unitOfWork = unitOfWork;
		_classifiedAdRepository = classifiedAdRepository;
	}


	public async Task<Guid> Handle(CreateClassifiedAdCommand request, CancellationToken cancellationToken)
	{
		var exists = await _classifiedAdRepository.ExistsAsync(new ClassifiedAdId(request.Id));

		if (exists)
		{
			throw new InvalidOperationException($"Entity with id {request.Id} already exists");
		}

		var classifiedAd = new ClassifiedAd(new ClassifiedAdId(request.Id), new UserProfileId(request.OwnerId));

		await _classifiedAdRepository.AddAsync(classifiedAd);

		await _unitOfWork.Commit();

		return request.Id;
	}
}
