using Framework.Application.Mediator;

namespace Marketplace.Application.Contracts.ClassifiedAds.Commands.V1
{
	public class UpdateClassifiedAdCommand : ICommand
	{
		public Guid Id { get; set; }

		public string Title { get; set; }

		public decimal Price { get; set; }

		public string Currency { get; set; }

		public string Text { get; set; }
	}
}
