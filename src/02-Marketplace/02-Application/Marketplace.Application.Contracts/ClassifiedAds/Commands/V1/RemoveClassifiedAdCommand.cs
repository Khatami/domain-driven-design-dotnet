﻿using Framework.Application.Mediator;

namespace Marketplace.Application.Contracts.ClassifiedAds.Commands.V1
{
	public class RemoveClassifiedAdCommand : ICommand
	{
		public Guid Id { get; set; }
	}
}
