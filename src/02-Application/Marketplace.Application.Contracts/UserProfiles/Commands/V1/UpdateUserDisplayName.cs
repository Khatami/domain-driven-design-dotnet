﻿namespace Marketplace.Application.Contracts.UserProfiles.Commands.V1
{
    public class UpdateUserDisplayName
    {
        public Guid UserId { get; set; }

        public string DisplayName { get; set; }
    }
}
