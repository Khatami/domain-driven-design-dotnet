using Marketplace.Application.Contracts.ClassifiedAds.V1;
using Marketplace.Application.Helpers;

namespace Marketplace.Application.ClassifiedAds.Commands
{
    internal class CreateClassifiedAdCommandHandler : IHandleCommand<ClassifiedAd_Create_V1>
    {
        public Task Handle(ClassifiedAd_Create_V1 command)
        {
            return Task.CompletedTask;
        }
    }
}
