﻿using Framework.Mediator.MediatR;
using Marketplace.Application.Contracts.UserProfiles.Commands.V1;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Api.Controllers;

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
	public async Task<IActionResult> Post(RegisterUserCommand request, CancellationToken cancellationToken)
	{
		await _mediator.Send(request, cancellationToken);

		return Ok();
	}

	[HttpPut]
	[Route("fullName")]
	public async Task<IActionResult> Put(UpdateUserFullNameCommand request, CancellationToken cancellationToken)
	{
		await _mediator.Send(request, cancellationToken);

		return Ok();
	}

	[HttpPut]
	[Route("displayName")]
	public async Task<IActionResult> Put(UpdateUserDisplayNameCommand request, CancellationToken cancellationToken)
	{
		await _mediator.Send(request, cancellationToken);

		return Ok();
	}

	[HttpPut]
	[Route("photo")]
	public async Task<IActionResult> Put(UpdateUserProfilePhotoCommand request, CancellationToken cancellationToken)
	{
		await _mediator.Send(request, cancellationToken);

		return Ok();
	}

	[HttpDelete]
	[Route("remove")]
	public async Task<IActionResult> Remove(RemoveUserCommand request, CancellationToken cancellationToken)
	{
		await _mediator.Send(request, cancellationToken);

		return Ok();
	}
}
