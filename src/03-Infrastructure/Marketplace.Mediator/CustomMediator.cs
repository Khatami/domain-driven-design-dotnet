using System;

namespace Marketplace.Mediator;

public class CustomMediator : MediatR.Mediator, ICustomMediator
{
	public CustomMediator(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}
}
