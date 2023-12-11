using Marketplace.Application.Contracts.UserProfiles.Commands.V1;
using Marketplace.Application.Infrastructure.Mediator;
using Marketplace.Domain.Shared.ValueObjects;
using Marketplace.Domain.UserProfiles;
using Marketplace.Domain.UserProfiles.ValueObjects;

namespace Marketplace.Application.UserProfiles.CommandHandlers;

internal class UpdateUserFullNameCommandHandler : ICommandHandler<UpdateUserFullNameCommand>
{
	private readonly IUserProfileRepository _userProfileRepository;

	public UpdateUserFullNameCommandHandler(IUserProfileRepository userProfileRepository)
	{
		_userProfileRepository = userProfileRepository;
	}

	public async Task Handle(UpdateUserFullNameCommand request, CancellationToken cancellationToken)
	{
		var userProfile = await _userProfileRepository.Load(new UserProfileId(request.UserId));

		if (userProfile == null)
		{
			throw new InvalidOperationException($"Entity with id {request.UserId} cannot be found");
		}

		userProfile.UpdateFullName(FullName.FromString(request.FullName));

		//await _unitOfWork.Commit();
	}
}
