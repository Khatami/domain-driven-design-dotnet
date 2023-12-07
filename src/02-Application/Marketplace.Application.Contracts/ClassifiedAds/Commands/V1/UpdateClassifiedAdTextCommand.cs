using Marketplace.Application.Contracts.Infrastructure;

namespace Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;

public class UpdateClassifiedAdTextCommand : IRequest
{
	public Guid Id { get; set; }

	public string Text { get; set; }
}