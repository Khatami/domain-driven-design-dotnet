using Marketplace.Application.Contracts.Infrastructure;

namespace Marketplace.Application.Contracts.UserProfiles.Commands.V1;

public class RegisterUserCommand : IRequest
{
	public Guid UserId { get; set; }

	public string FullName { get; set; }

	public string DisplayName { get; set; }
}
