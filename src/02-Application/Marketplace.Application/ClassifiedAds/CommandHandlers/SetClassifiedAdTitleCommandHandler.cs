﻿using Marketplace.Domain.ClassifiedAds;
using Marketplace.Application.Infrastructure;
using Marketplace.Domain.Shared.ValueObjects;
using Marketplace.Domain.ClassifiedAds.ValueObjects;
using Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;

namespace Marketplace.Application.ClassifiedAds.CommandHandlers;

internal class SetClassifiedAdTitleCommandHandler : Mediator.ICommandHandler<SetClassifiedAdTitleCommand>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IClassifiedAdRepository _classifiedAdRepository;

	public SetClassifiedAdTitleCommandHandler(IUnitOfWork unitOfWork, IClassifiedAdRepository classifiedAdRepository)
	{
		_unitOfWork = unitOfWork;
		_classifiedAdRepository = classifiedAdRepository;
	}

	public async Task Handle(SetClassifiedAdTitleCommand request, CancellationToken cancellationToken)
	{
		var classifiedAd = await _classifiedAdRepository.Load(new ClassifiedAdId(request.Id));

		if (classifiedAd == null)
		{
			throw new InvalidOperationException($"Entity with id {request.Id} cannot be found");
		}

		classifiedAd.SetTitle(ClassifiedAdTitle.FromString(request.Title));

		await _unitOfWork.Commit();
	}
}
