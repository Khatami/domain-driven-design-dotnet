using Microsoft.AspNetCore.Mvc;
using Marketplace.Application.Contracts.UserProfiles.Commands.V1;
using Marketplace.Application.Contracts.Infrastructure;

namespace Marketplace.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserProfileController : ControllerBase
{
	private readonly IMediator _mediator;

	public UserProfileController(IMediator mediator)
	{
		_mediator = mediator;
	}

	//private readonly IUserProfileApplicationService _userProfileApplicationService;

	//public UserProfileController(IUserProfileApplicationService userProfileApplicationService)
	//{
	//	_userProfileApplicationService = userProfileApplicationService;
	//}

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
