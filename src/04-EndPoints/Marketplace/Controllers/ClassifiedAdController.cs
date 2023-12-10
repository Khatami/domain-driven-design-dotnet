using Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;
using Marketplace.Application.Infrastructure.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Controllers;

[ApiController]
[Route("api/V1/[controller]")]
public class ClassifiedAdController : Controller
{
	private readonly IApplicationMediator _mediator;

	public ClassifiedAdController(IApplicationMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	public async Task<IActionResult> Post(CreateClassifiedAdCommand request)
	{
		var result = await _mediator.Send<CreateClassifiedAdCommand, Guid>(request);

		// This code is going to show compiled error, because it's a command with response
		//await _mediator.Send(request);

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
