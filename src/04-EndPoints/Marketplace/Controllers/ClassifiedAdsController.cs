using Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;
using Marketplace.Application.Contracts.ClassifiedAds.IServices;
using Marketplace.Application.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Controllers
{
    [Route("api/V1/[controller]")]
	[ApiController]
	public class ClassifiedAdsController : Controller
	{
		private readonly IHandleCommand<CreateClassifiedAd_V1> _createAdCommandHandler;
		private readonly IClassifiedAdService _classifiedAdService;

		public ClassifiedAdsController(IHandleCommand<CreateClassifiedAd_V1> createAdCommandHandler,
			IClassifiedAdService classifiedAdService)
		{
			_createAdCommandHandler = createAdCommandHandler;
			_classifiedAdService = classifiedAdService;
		}

		[HttpPost]
		public async Task<IActionResult> Post(CreateClassifiedAd_V1 request)
		{
			// await _createAdCommandHandler.Handle(request);

			await _classifiedAdService.Handle(request);

			return Ok();
		}

		[Route("name")]
		[HttpPut]
		public async Task<IActionResult> Put(SetClassifiedAdTitle_V1 request)
		{
			await _classifiedAdService.Handle(request);

			return Ok();
		}

		[Route("text")]
		[HttpPut]
		public async Task<IActionResult> Put(UpdateClassifiedAdText_V1 request)
		{
			await _classifiedAdService.Handle(request);

			return Ok();
		}

		[Route("price")]
		[HttpPut]
		public async Task<IActionResult> Put(UpdateClassifiedAdPrice_V1 request)
		{
			await _classifiedAdService.Handle(request);

			return Ok();
		}

		[Route("publish")]
		[HttpPut]
		public async Task<IActionResult> Put(RequestClassifiedAdToPublish_V1 request)
		{
			await _classifiedAdService.Handle(request);

			return Ok();
		}
	}
}
