using Marketplace.Mediator;
using Marketplace.Application.Contracts.UserProfiles.IServices;
using Marketplace.Application.Contracts.UserProfiles.Commands.V1;

namespace Marketplace.Application.UserProfiles.CommandHandlers;

internal class UpdateUserProfilePhotoCommandHandler : ICommandHandler<UpdateUserProfilePhotoCommand>
{
	private readonly IUpdateUserProfileService _updateUserProfileService;

	public UpdateUserProfilePhotoCommandHandler(IUpdateUserProfileService updateUserProfileService)
	{
		_updateUserProfileService = updateUserProfileService;
	}

	public async Task Handle(UpdateUserProfilePhotoCommand request, CancellationToken cancellationToken)
	{
		await _updateUserProfileService.HandleUpdate(request.UserId, profile => profile.UpdateProfilePhoto(new Uri(request.PhotoUrl)));
	}
}
