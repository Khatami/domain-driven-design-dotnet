using Marketplace.Application.Contracts.ClassifiedAds.IServices;

namespace Marketplace.Application.ClassifiedAds.Services
{
	/// <summary>
	/// It's completely against SRP
	/// </summary>
	internal class ClassifiedAdService : IClassifiedAdService
	{
		public Task Handle(object command)
		{
			return Task.CompletedTask;
		}
	}
}
