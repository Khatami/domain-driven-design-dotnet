namespace Marketplace.Application.Contracts.ClassifiedAds.IServices
{
	/// <summary>
	/// It's completely against SRP
	/// </summary>
	public interface IClassifiedAdApplicationService
	{
		Task Handle(object command);
	}
}
