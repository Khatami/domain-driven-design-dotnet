namespace Marketplace.Application.Contracts.UserProfiles.Commands.V1
{
    public class UpdateUserFullName
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
    }
}
