using Marketplace.Application.SeedWork.Mediator;

namespace Marketplace.Application.Contracts.ClassifiedAds.Commands.V1
{
	public class SnapshotClassifiedAdCommand : ICommand
	{
		public Guid Id { get; set; }
	}
}