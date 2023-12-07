namespace Marketplace.Application.Contracts.UserProfiles.IServices;

/// <summary>
/// the implementation completely against SRP
/// </summary>
public interface IUserProfileApplicationService
{
	Task Handle(object command);
}
