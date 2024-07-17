using Framework.Application.Mediator;
using Framework.Application.UnitOfWork;
using Marketplace.Application.Contracts.UserProfiles.Commands.V1;
using Marketplace.Domain.Shared.ValueObjects;
using Marketplace.Domain.UserProfiles;
using Marketplace.Domain.UserProfiles.Delegates;
using Marketplace.Domain.UserProfiles.ValueObjects;

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
		var fetchUserProfile = await _repository.GetAsync(new UserProfileId(request.UserId));

		if (fetchUserProfile != null)
		{
			if (fetchUserProfile.IsDeleted)
			{
				throw new InvalidOperationException($"Entity with id {request.UserId} has been removed");
			}

			throw new InvalidOperationException($"Entity with id {request.UserId} already exists");
		}

		var userProfile = new UserProfile
			(new UserProfileId(request.UserId),
			FullName.FromString(request.FullName),
			DisplayName.FromString(request.DisplayName, _checkText));

		await _repository.AddAsync(userProfile);

		await _unitOfWork.Commit(cancellationToken);
	}
}
