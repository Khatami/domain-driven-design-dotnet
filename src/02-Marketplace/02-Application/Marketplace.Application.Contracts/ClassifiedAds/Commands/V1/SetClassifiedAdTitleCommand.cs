using Framework.Application.Mediator;

namespace Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;

public class SetClassifiedAdTitleCommand : ICommand
{
	public Guid Id { get; set; }

	public string Title { get; set; }
}
