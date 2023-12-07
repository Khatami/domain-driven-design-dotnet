using Marketplace.Application.Contracts.Infrastructure;

namespace Marketplace.Application.Contracts.UserProfiles.Commands.V1;

public class UpdateUserFullNameCommand : IRequest
{
	public Guid UserId { get; set; }

	public string FullName { get; set; }
}
