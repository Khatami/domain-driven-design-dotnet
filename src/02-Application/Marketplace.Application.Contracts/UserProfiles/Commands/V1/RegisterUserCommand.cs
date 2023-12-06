namespace Marketplace.Application.Contracts.UserProfiles.Commands.V1;

public class RegisterUserCommand : Mediator.ICommand
{
	public Guid UserId { get; set; }

	public string FullName { get; set; }

	public string DisplayName { get; set; }
}
