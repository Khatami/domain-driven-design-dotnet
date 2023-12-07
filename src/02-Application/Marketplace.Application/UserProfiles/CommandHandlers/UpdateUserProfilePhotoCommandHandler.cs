using Marketplace.Application.Contracts.UserProfiles.Commands.V1;
using Marketplace.Application.Infrastructure.Mediator;
using Marketplace.Domain.Shared.ValueObjects;
using Marketplace.Domain.UserProfiles;

namespace Marketplace.Application.UserProfiles.CommandHandlers;

internal class UpdateUserProfilePhotoCommandHandler : ICommandHandler<UpdateUserProfilePhotoCommand>
{
	private readonly IUserProfileRepository _userProfileRepository;

	public UpdateUserProfilePhotoCommandHandler(IUserProfileRepository userProfileRepository)
	{
		_userProfileRepository = userProfileRepository;
	}

	public async Task Handle(UpdateUserProfilePhotoCommand request, CancellationToken cancellationToken)
	{
		var userProfile = await _userProfileRepository.Load(new UserId(request.UserId));

		if (userProfile == null)
		{
			throw new InvalidOperationException($"Entity with id {request.UserId} cannot be found");
		}

		userProfile.UpdateProfilePhoto(new Uri(request.PhotoUrl));

		//await _unitOfWork.Commit();
	}
}
