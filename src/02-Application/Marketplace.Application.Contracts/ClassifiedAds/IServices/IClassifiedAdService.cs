namespace Marketplace.Application.Contracts.ClassifiedAds.IServices
{
	/// <summary>
	/// It's completely against SRP
	/// </summary>
	public interface IClassifiedAdService
	{
		Task Handle(object command);
	}
}
