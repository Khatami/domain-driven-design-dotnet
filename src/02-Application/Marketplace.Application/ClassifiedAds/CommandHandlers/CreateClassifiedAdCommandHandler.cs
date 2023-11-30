using Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;
using Marketplace.Application.Shared;

namespace Marketplace.Application.ClassifiedAds.CommandHandlers
{
	internal class CreateClassifiedAdCommandHandler : IHandleCommand<CreateClassifiedAd>
	{
		public Task Handle(CreateClassifiedAd command)
		{
			return Task.CompletedTask;
		}
	}
}
