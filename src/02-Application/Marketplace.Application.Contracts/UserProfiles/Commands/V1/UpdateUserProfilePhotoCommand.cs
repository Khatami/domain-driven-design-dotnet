using Marketplace.Application.SeedWork.Mediator;

namespace Marketplace.Application.Contracts.UserProfiles.Commands.V1;

public class UpdateUserProfilePhotoCommand : ICommand
{
	public Guid UserId { get; set; }

	public string PhotoUrl { get; set; }
}
