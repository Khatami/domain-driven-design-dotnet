using Framework.Application.Mediator;

namespace Marketplace.Application.Contracts.UserProfiles.Commands.V1;

public class UpdateUserDisplayNameCommand : ICommand
{
	public Guid UserId { get; set; }

	public required string DisplayName { get; set; }
}
