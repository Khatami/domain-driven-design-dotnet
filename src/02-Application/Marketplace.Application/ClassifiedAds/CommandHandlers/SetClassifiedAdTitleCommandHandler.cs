using Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;
using Marketplace.Application.Infrastructure;
using Marketplace.Domain.ClassifiedAds.ValueObjects;
using Marketplace.Domain.ClassifiedAds;
using Marketplace.Domain.Shared.ValueObjects;

namespace Marketplace.Application.ClassifiedAds.CommandHandlers;

internal class SetClassifiedAdTitleCommandHandler : Mediator.ICommandHandler<SetClassifiedAdTitleCommand>
{
	public Task Handle(SetClassifiedAdTitleCommand request, CancellationToken cancellationToken)
	{
		classifiedAd = await _classifiedAdRepository.Load(new ClassifiedAdId(cmd.Id));

		if (classifiedAd == null)
			throw new InvalidOperationException($"Entity with id {cmd.Id} cannot be found");

		classifiedAd.SetTitle(ClassifiedAdTitle.FromString(cmd.Title));

		await _unitOfWork.Commit();
		break;
	}
}
