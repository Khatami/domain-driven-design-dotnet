using Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;
using Marketplace.Application.Helpers;

namespace Marketplace.Application.ClassifiedAds.CommandHandlers
{
	internal class CreateClassifiedAdCommandHandler : IHandleCommand<ClassifiedAd_Create_V1>
	{
		public Task Handle(ClassifiedAd_Create_V1 command)
		{
			return Task.CompletedTask;
		}
	}
}
