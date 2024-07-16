using Framework.Application.Mediator;

namespace Marketplace.Application.Contracts.ClassifiedAds.Commands.V1
{
	public class UpdateClassifiedAdCommand : ICommand
	{
		public Guid Id { get; set; }

		public required string Title { get; set; }

		public decimal Price { get; set; }

		public required string Currency { get; set; }

		public required string Text { get; set; }
	}
}
