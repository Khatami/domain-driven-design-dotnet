using Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;
using Marketplace.Application.Infrastructure;
using Marketplace.Domain.ClassifiedAds.ValueObjects;
using Marketplace.Domain.ClassifiedAds;
using Marketplace.Domain.Shared.ValueObjects;

namespace Marketplace.Application.ClassifiedAds.CommandHandlers;

internal class UpdateClassifiedAdTextCommandHandler : Mediator.ICommandHandler<UpdateClassifiedAdTextCommand>
{
	public Task Handle(UpdateClassifiedAdTextCommand request, CancellationToken cancellationToken)
	{
		classifiedAd = await _classifiedAdRepository.Load(new ClassifiedAdId(cmd.Id));

		if (classifiedAd == null)
			throw new InvalidOperationException($"Entity with id {cmd.Id} cannot be found");

		classifiedAd.UpdateText(ClassifiedAdText.FromString(cmd.Text));

		await _unitOfWork.Commit();
		break;
	}
}
