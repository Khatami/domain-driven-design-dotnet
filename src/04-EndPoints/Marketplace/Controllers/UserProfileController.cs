using Marketplace.Application.Contracts.UserProfiles.Commands.V1;
using Marketplace.Application.Contracts.UserProfiles.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserProfileController : ControllerBase
	{
		private readonly IUserProfileApplicationService _userProfileApplicationService;

		public UserProfileController(IUserProfileApplicationService userProfileApplicationService)
		{
			_userProfileApplicationService = userProfileApplicationService;
		}

		[HttpPost]
		public async Task<IActionResult> Post(RegisterUserCommand request)
		{
			await _userProfileApplicationService.Handle(request);

			return Ok();
		}

		[Route("fullname")]
		[HttpPut]
		public async Task<IActionResult> Put(UpdateUserFullNameCommand request)
		{
			await _userProfileApplicationService.Handle(request);

			return Ok();
		}

		[Route("displayname")]
		[HttpPut]
		public async Task<IActionResult> Put(UpdateUserDisplayNameCommand request)
		{
			await _userProfileApplicationService.Handle(request);

			return Ok();
		}

		[Route("photo")]
		[HttpPut]
		public async Task<IActionResult> Put(UpdateUserProfilePhotoCommand request)
		{
			await _userProfileApplicationService.Handle(request);

			return Ok();
		}
	}
}
