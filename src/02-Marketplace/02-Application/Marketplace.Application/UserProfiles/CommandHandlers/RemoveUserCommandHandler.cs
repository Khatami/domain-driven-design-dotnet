using Framework.Application.Mediator;
using Framework.Application.UnitOfWork;
using Marketplace.Application.Contracts.UserProfiles.Commands.V1;
using Marketplace.Domain.Shared.ValueObjects;
using Marketplace.Domain.UserProfiles;

namespace Marketplace.Application.UserProfiles.CommandHandlers
{
	internal class RemoveUserCommandHandler : ICommandHandler<RemoveUserCommand>
	{
		private readonly IUserProfileRepository _userProfileRepository;
		private readonly IUnitOfWork _unitOfWork;
		public RemoveUserCommandHandler(IUserProfileRepository userProfileRepository, IUnitOfWork unitOfwork)
		{
			_userProfileRepository = userProfileRepository;
			_unitOfWork = unitOfwork;
		}

		public async Task Handle(RemoveUserCommand request, CancellationToken cancellationToken)
		{
			var userProfile = await _userProfileRepository.GetAsync(new UserProfileId(request.UserId));

			if (userProfile == null)
			{
				throw new InvalidOperationException($"Entity with id {request.UserId} cannot be found");
			}

			if (userProfile.IsDeleted)
			{
				throw new InvalidOperationException($"Entity with id {userProfile.Id} has been removed");
			}

			userProfile.Remove();

			await _unitOfWork.Commit(cancellationToken);
		}
	}
}
