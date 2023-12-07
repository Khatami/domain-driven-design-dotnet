using Marketplace.Mediator;
using Marketplace.Domain.UserProfiles.Delegates;
using Marketplace.Domain.UserProfiles.ValueObjects;
using Marketplace.Application.Contracts.UserProfiles.IServices;
using Marketplace.Application.Contracts.UserProfiles.Commands.V1;

namespace Marketplace.Application.UserProfiles.CommandHandlers;

internal class UpdateUserFullNameCommandHandler : ICommandHandler<UpdateUserFullNameCommand>
{
	private readonly IUpdateUserProfileService _updateUserProfileService;

	public UpdateUserFullNameCommandHandler(IUpdateUserProfileService updateUserProfileService)
	{
		_updateUserProfileService = updateUserProfileService;
	}

	public async Task Handle(UpdateUserFullNameCommand request, CancellationToken cancellationToken)
	{
		await _updateUserProfileService.HandleUpdate(request.UserId, profile => profile.UpdateFullName(FullName.FromString(request.FullName)));
	}
}
