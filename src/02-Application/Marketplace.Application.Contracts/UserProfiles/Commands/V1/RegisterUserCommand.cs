using Marketplace.Application.SeedWork.Mediator;

namespace Marketplace.Application.Contracts.UserProfiles.Commands.V1;

public class RegisterUserCommand : ICommand
{
	public Guid UserId { get; set; } = Guid.NewGuid();

	public string FullName { get; set; }

	public string DisplayName { get; set; }
}
