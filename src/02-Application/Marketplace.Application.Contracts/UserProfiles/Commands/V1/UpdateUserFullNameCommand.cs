namespace Marketplace.Application.Contracts.UserProfiles.Commands.V1;

public class UpdateUserFullNameCommand : Mediator.ICommand
{
	public Guid UserId { get; set; }

	public string FullName { get; set; }
}
