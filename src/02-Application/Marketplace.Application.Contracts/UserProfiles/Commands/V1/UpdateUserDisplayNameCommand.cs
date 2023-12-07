using Marketplace.Application.Contracts.Infrastructure;

namespace Marketplace.Application.Contracts.UserProfiles.Commands.V1;

public class UpdateUserDisplayNameCommand : IRequest
{
	public Guid UserId { get; set; }

	public string DisplayName { get; set; }
}
