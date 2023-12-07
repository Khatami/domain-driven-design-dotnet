using Marketplace.Mediator;
using Marketplace.Domain.UserProfiles.Delegates;
using Marketplace.Domain.UserProfiles.ValueObjects;
using Marketplace.Application.Contracts.UserProfiles.IServices;
using Marketplace.Application.Contracts.UserProfiles.Commands.V1;

namespace Marketplace.Application.UserProfiles.CommandHandlers;

internal class UpdateUserDisplayNameCommandHandler : ICommandHandler<UpdateUserDisplayNameCommand>
{
	private readonly CheckTextForProfanity _checkText;
	private readonly IUpdateUserProfileService _updateUserProfileService;

	public UpdateUserDisplayNameCommandHandler(CheckTextForProfanity checkText, IUpdateUserProfileService updateUserProfileService)
	{
		_checkText = checkText;
		_updateUserProfileService = updateUserProfileService;
	}

	public async Task Handle(UpdateUserDisplayNameCommand request, CancellationToken cancellationToken)
	{
		await _updateUserProfileService.HandleUpdate
			(request.UserId, profile => profile.UpdateDisplayName(DisplayName.FromString(request.DisplayName, _checkText)));
	}
}
