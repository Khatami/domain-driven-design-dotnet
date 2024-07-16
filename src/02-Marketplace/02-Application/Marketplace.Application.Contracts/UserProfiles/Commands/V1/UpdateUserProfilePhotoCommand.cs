using Framework.Application.Mediator;

namespace Marketplace.Application.Contracts.UserProfiles.Commands.V1;

public class UpdateUserProfilePhotoCommand : ICommand
{
	public Guid UserId { get; set; }

	public required string PhotoUrl { get; set; }
}
