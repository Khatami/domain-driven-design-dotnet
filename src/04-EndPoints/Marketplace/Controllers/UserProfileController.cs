using Marketplace.Application.Contracts.UserProfiles.Commands.V1;
using Marketplace.Application.Contracts.UserProfiles.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserProfileController : ControllerBase
	{
		[Route("/profile")]
		public class UserProfileCommandsApi : Controller
		{
			private readonly IUserProfileApplicationService _userProfileApplicationService;

			public UserProfileCommandsApi(IUserProfileApplicationService userProfileApplicationService)
			{
				_userProfileApplicationService = userProfileApplicationService;
			}

			[HttpPost]
			public async Task<IActionResult> Post(RegisterUser request)
			{
				await _userProfileApplicationService.Handle(request);

				return Ok();
			}

			[Route("fullname")]
			[HttpPut]
			public async Task<IActionResult> Put(UpdateUserFullName request)
			{
				await _userProfileApplicationService.Handle(request);

				return Ok();
			}

			[Route("displayname")]
			[HttpPut]
			public async Task<IActionResult> Put(UpdateUserDisplayName request)
			{
				await _userProfileApplicationService.Handle(request);

				return Ok();
			}

			[Route("photo")]
			[HttpPut]
			public async Task<IActionResult> Put(UpdateUserProfilePhoto request)
			{
				await _userProfileApplicationService.Handle(request);

				return Ok();
			}
		}
	}
}
