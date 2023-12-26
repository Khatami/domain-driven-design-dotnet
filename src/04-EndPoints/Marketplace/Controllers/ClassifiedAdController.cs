﻿using Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;
using Marketplace.Application.SeedWork.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
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
		//var result = await _mediator.Send<CreateClassifiedAdCommand, Guid>(request);

		var result = await _mediator.Send(request);

		return Ok();
	}

	[HttpPatch]
	[Route("name")]
	public async Task<IActionResult> Put(SetClassifiedAdTitleCommand request)
	{
		await _mediator.Send(request);

		return Ok();
	}

	[HttpPatch]
	[Route("text")]
	public async Task<IActionResult> Put(UpdateClassifiedAdTextCommand request)
	{
		await _mediator.Send(request);

		return Ok();
	}

	[HttpPatch]
	[Route("price")]
	public async Task<IActionResult> Put(UpdateClassifiedAdCommand request)
	{
		await _mediator.Send(request);

		return Ok();
	}

	[HttpPut]
	public async Task<IActionResult> Pub(UpdateClassifiedAdCommand request)
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
