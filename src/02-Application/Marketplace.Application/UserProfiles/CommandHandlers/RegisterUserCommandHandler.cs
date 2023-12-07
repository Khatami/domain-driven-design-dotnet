﻿using Marketplace.Mediator;
using Marketplace.Application.Contracts.UserProfiles.Commands.V1;
using Marketplace.Application.Infrastructure;
using Marketplace.Domain.Shared.ValueObjects;
using Marketplace.Domain.UserProfiles.ValueObjects;
using Marketplace.Domain.UserProfiles;
using Marketplace.Domain.UserProfiles.Delegates;

namespace Marketplace.Application.UserProfiles.CommandHandlers;

internal class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly CheckTextForProfanity _checkText;
	private readonly IUserProfileRepository _repository;

	public RegisterUserCommandHandler
		(IUnitOfWork unitOfWork, CheckTextForProfanity checkText, IUserProfileRepository repository)
	{
		_checkText = checkText;
		_repository = repository;
		_unitOfWork = unitOfWork;
	}

	public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
	{
		var isExists = await _repository.Exists(new UserId(request.UserId));

		if (isExists)
		{
			throw new InvalidOperationException($"Entity with id {request.UserId} already exists");
		}

		var userProfile = new UserProfile
			(new UserId(request.UserId),
			FullName.FromString(request.FullName),
			DisplayName.FromString(request.DisplayName, _checkText));

		await _repository.Add(userProfile);

		await _unitOfWork.Commit();
	}
}