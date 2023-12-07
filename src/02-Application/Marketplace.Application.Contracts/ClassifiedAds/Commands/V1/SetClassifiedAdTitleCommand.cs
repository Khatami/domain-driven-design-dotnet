using Marketplace.Application.Contracts.Infrastructure;

namespace Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;

public class SetClassifiedAdTitleCommand : IRequest
{
	public Guid Id { get; set; }

	public string Title { get; set; }
}
