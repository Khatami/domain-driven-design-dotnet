using Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;
using Marketplace.Application.Infrastructure;
using Marketplace.Application.Infrastructure.Mediator;
using Marketplace.Domain.ClassifiedAds;
using Marketplace.Domain.ClassifiedAds.ValueObjects;
using Marketplace.Domain.Shared.ValueObjects;

namespace Marketplace.Application.ClassifiedAds.CommandHandlers;

internal class UpdateClassifiedAdTextCommandHandler : ICommandHandler<UpdateClassifiedAdTextCommand>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IClassifiedAdRepository _classifiedAdRepository;

	public UpdateClassifiedAdTextCommandHandler(IUnitOfWork unitOfWork, IClassifiedAdRepository classifiedAdRepository)
	{
		MediatR.Mediator a;
		_unitOfWork = unitOfWork;
		_classifiedAdRepository = classifiedAdRepository;
	}

	public async Task Handle(UpdateClassifiedAdTextCommand request, CancellationToken cancellationToken)
	{
		var classifiedAd = await _classifiedAdRepository.Load(new ClassifiedAdId(request.Id));

		if (classifiedAd == null)
		{
			throw new InvalidOperationException($"Entity with id {request.Id} cannot be found");
		}

		classifiedAd.UpdateText(ClassifiedAdText.FromString(request.Text));

		await _unitOfWork.Commit();
	}
}
