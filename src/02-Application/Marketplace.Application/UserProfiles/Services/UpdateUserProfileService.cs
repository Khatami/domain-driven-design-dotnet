using Marketplace.Application.Contracts.UserProfiles.IServices;
using Marketplace.Application.Infrastructure;
using Marketplace.Domain.UserProfiles;
using Marketplace.Domain.Shared.ValueObjects;

namespace Marketplace.Application.UserProfiles.Services;

internal class UpdateUserProfileService : IUpdateUserProfileService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IUserProfileRepository _repository;

	public UpdateUserProfileService
		(IUnitOfWork unitOfWork, IUserProfileRepository repository)
	{
		_repository = repository;
		_unitOfWork = unitOfWork;
	}

	public async Task HandleUpdate(Guid userProfileId, Action<UserProfile> operation)
	{
		var userProfile = await _repository.Load(new UserId(userProfileId));

		if (userProfile == null)
		{
			throw new InvalidOperationException($"Entity with id {userProfileId} cannot be found");
		}

		operation(userProfile);

		await _unitOfWork.Commit();
	}
}
