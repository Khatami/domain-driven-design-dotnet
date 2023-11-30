namespace Marketplace.Application.Contracts.UserProfiles.Commands.V1
{
    public class RegisterUser
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string DisplayName { get; set; }
    }
}
