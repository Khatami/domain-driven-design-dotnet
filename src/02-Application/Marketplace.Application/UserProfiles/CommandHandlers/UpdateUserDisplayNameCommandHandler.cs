﻿using Marketplace.Application.Contracts.UserProfiles.Commands.V1;
using Marketplace.Domain.Shared.ValueObjects;
using Marketplace.Domain.UserProfiles;
using Marketplace.Domain.UserProfiles.Delegates;
using Marketplace.Domain.UserProfiles.ValueObjects;
using Marketplace.Mediator;

namespace Marketplace.Application.UserProfiles.CommandHandlers;

internal class UpdateUserDisplayNameCommandHandler : ICommandHandler<UpdateUserDisplayNameCommand>
{
	private readonly CheckTextForProfanity _checkText;
	private readonly IUserProfileRepository _userProfileRepository;

	public UpdateUserDisplayNameCommandHandler(CheckTextForProfanity checkText, 
		IUserProfileRepository userProfileRepository)
	{
		_checkText = checkText;
		_userProfileRepository = userProfileRepository;
	}

	public async Task Handle(UpdateUserDisplayNameCommand request, CancellationToken cancellationToken)
	{
		var userProfile = await _userProfileRepository.Load(new UserId(request.UserId));

		if (userProfile == null)
		{
			throw new InvalidOperationException($"Entity with id {request.UserId} cannot be found");
		}

		userProfile.UpdateDisplayName(DisplayName.FromString(request.DisplayName, _checkText));

		//await _unitOfWork.Commit();
	}
}