namespace Marketplace.Application.Contracts.ClassifiedAds.IServices
{
    /// <summary>
    /// the implementation completely against SRP
    /// </summary>
    public interface IUserProfileApplicationService
	{
        Task Handle(object command);
    }
}
