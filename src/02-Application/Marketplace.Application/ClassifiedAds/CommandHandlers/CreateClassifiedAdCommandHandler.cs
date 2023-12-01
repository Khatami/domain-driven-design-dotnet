using Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;
using Marketplace.Application.Infrastructure;

namespace Marketplace.Application.ClassifiedAds.CommandHandlers
{
	// It's a sample for using commandhandlers instead of application service way
	internal class CreateClassifiedAdCommandHandler : IHandleCommand<CreateClassifiedAd>
	{
		public Task Handle(CreateClassifiedAd command)
		{
			return Task.CompletedTask;
		}
	}
}
