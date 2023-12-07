using Marketplace.Mediator;
using Microsoft.AspNetCore.Mvc;
using Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;

namespace Marketplace.Controllers;

[ApiController]
[Route("api/V1/[controller]")]
public class ClassifiedAdController : Controller
{
	private readonly ICustomMediator _mediator;

	//private readonly IClassifiedAdApplicationService _classifiedAdService;
	//private readonly IHandleCommand<CreateClassifiedAdCommand> _createAdCommandHandler;

	//public ClassifiedAdController
	//	(IClassifiedAdApplicationService classifiedAdService,
	//	IHandleCommand<CreateClassifiedAdCommand> createAdCommandHandler)
	//{
	//	_classifiedAdService = classifiedAdService;
	//	_createAdCommandHandler = createAdCommandHandler;
	//}

	public ClassifiedAdController(ICustomMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	public async Task<IActionResult> Post(CreateClassifiedAdCommand request)
	{
		await _mediator.Send(request);

		return Ok();
	}

	[HttpPut]
	[Route("name")]
	public async Task<IActionResult> Put(SetClassifiedAdTitleCommand request)
	{
		await _mediator.Send(request);

		return Ok();
	}

	[HttpPut]
	[Route("text")]
	public async Task<IActionResult> Put(UpdateClassifiedAdTextCommand request)
	{
		await _mediator.Send(request);

		return Ok();
	}

	[HttpPut]
	[Route("price")]
	public async Task<IActionResult> Put(UpdateClassifiedAdPriceCommand request)
	{
		await _mediator.Send(request);

		return Ok();
	}

	[HttpPut]
	[Route("publish")]
	public async Task<IActionResult> Put(RequestClassifiedAdToPublishCommand request)
	{
		await _mediator.Send(request);

		return Ok();
	}
}
