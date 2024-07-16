using Framework.Application.Mediator;

namespace Marketplace.Application.Contracts.UserProfiles.Commands.V1
{
	public class RemoveUserCommand : ICommand
	{
		public Guid UserId { get; set; }
	}
}
