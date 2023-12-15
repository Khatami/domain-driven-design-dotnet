﻿using Marketplace.Application.Contracts.UserProfiles.Commands.V1;
using Marketplace.Application.Infrastructure.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserProfileController : ControllerBase
{
	private readonly IApplicationMediator _mediator;

	public UserProfileController(IApplicationMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	public async Task<IActionResult> Post(RegisterUserCommand request)
	{
		await _mediator.Send(request);

		return Ok();
	}

	[HttpPut]
	[Route("fullName")]
	public async Task<IActionResult> Put(UpdateUserFullNameCommand request)
	{
		await _mediator.Send(request);

		return Ok();
	}

	[HttpPut]
	[Route("displayName")]
	public async Task<IActionResult> Put(UpdateUserDisplayNameCommand request)
	{
		await _mediator.Send(request);

		return Ok();
	}

	[HttpPut]
	[Route("photo")]
	public async Task<IActionResult> Put(UpdateUserProfilePhotoCommand request)
	{
		await _mediator.Send(request);

		return Ok();
	}
}
