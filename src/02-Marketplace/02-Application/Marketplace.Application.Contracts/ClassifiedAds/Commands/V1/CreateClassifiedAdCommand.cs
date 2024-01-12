using Framework.Application.Mediator;

namespace Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;

public class CreateClassifiedAdCommand : ICommandResponse<Guid>
{
	public Guid Id { get; set; } = Guid.NewGuid();

	public Guid OwnerId { get; set; }
}
