using Framework.Application.Mediator;

namespace Marketplace.Application.Contracts.UserProfiles.Commands.V1;

public class RegisterUserCommand : ICommand
{
	public Guid UserId { get; set; } = Guid.NewGuid();

	public required string FullName { get; set; }

	public required string DisplayName { get; set; }
}
