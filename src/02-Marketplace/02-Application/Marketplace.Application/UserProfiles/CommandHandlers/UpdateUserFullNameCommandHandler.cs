using Framework.Application.Mediator;
using Framework.Application.UnitOfWork;
using Marketplace.Application.Contracts.UserProfiles.Commands.V1;
using Marketplace.Domain.Shared.ValueObjects;
using Marketplace.Domain.UserProfiles;
using Marketplace.Domain.UserProfiles.ValueObjects;

namespace Marketplace.Application.UserProfiles.CommandHandlers;

internal class UpdateUserFullNameCommandHandler : ICommandHandler<UpdateUserFullNameCommand>
{
	private readonly IUserProfileRepository _userProfileRepository;
	private readonly IUnitOfWork _unitOfWork;
	public UpdateUserFullNameCommandHandler(IUserProfileRepository userProfileRepository, IUnitOfWork unitOfwork)
	{
		_userProfileRepository = userProfileRepository;
		_unitOfWork = unitOfwork;
	}

	public async Task Handle(UpdateUserFullNameCommand request, CancellationToken cancellationToken)
	{
		var userProfile = await _userProfileRepository.GetAsync(new UserProfileId(request.UserId));

		if (userProfile == null)
		{
			throw new InvalidOperationException($"Entity with id {request.UserId} cannot be found");
		}

		userProfile.UpdateFullName(FullName.FromString(request.FullName));

		await _unitOfWork.Commit(cancellationToken);
	}
}
