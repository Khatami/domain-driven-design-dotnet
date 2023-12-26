using Marketplace.Application.SeedWork.Mediator;

namespace Marketplace.Application.Contracts.UserProfiles.Commands.V1;

public class UpdateUserDisplayNameCommand : ICommand
{
	public Guid UserId { get; set; }

	public string DisplayName { get; set; }
}
