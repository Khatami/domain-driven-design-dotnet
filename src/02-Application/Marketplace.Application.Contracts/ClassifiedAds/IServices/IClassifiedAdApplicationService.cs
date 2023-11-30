﻿namespace Marketplace.Application.Contracts.ClassifiedAds.IServices
{
    /// <summary>
    /// the implementation completely against SRP
    /// </summary>
    public interface IClassifiedAdApplicationService
    {
        Task Handle(object command);
    }
}
