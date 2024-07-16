using Framework.Application.Mediator;

namespace Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;

public class UpdateClassifiedAdTextCommand : ICommand
{
	public Guid Id { get; set; }

	public required string Text { get; set; }
}