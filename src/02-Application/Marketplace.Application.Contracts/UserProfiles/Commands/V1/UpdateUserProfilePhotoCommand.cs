using Marketplace.Application.Contracts.Infrastructure;

namespace Marketplace.Application.Contracts.UserProfiles.Commands.V1;

public class UpdateUserProfilePhotoCommand : IRequest
{
	public Guid UserId { get; set; }

	public string PhotoUrl { get; set; }
}
