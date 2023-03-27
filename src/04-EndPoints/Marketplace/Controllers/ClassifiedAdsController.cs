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
		private readonly IHandleCommand<ClassifiedAd_Create_V1> _createAdCommandHandler;
		private readonly IClassifiedAdService _classifiedAdService;

		public ClassifiedAdsController(IHandleCommand<ClassifiedAd_Create_V1> createAdCommandHandler,
			IClassifiedAdService classifiedAdService)
		{
			_createAdCommandHandler = createAdCommandHandler;
			_classifiedAdService = classifiedAdService;
		}

		[HttpPost]
		public async Task<IActionResult> Post(ClassifiedAd_Create_V1 request)
		{
			// await _createAdCommandHandler.Handle(request);

			await _classifiedAdService.Handle(request);

			return Ok();
		}

		[Route("name")]
		[HttpPut]
		public async Task<IActionResult> Put(ClassifiedAd_SetTitle_V1 request)
		{
			await _classifiedAdService.Handle(request);

			return Ok();
		}

		[Route("text")]
		[HttpPut]
		public async Task<IActionResult> Put(ClassifiedAd_UpdateText_V1 request)
		{
			await _classifiedAdService.Handle(request);

			return Ok();
		}

		[Route("price")]
		[HttpPut]
		public async Task<IActionResult> Put(ClassifiedAd_UpdatePrice_V1 request)
		{
			await _classifiedAdService.Handle(request);

			return Ok();
		}

		[Route("publish")]
		[HttpPut]
		public async Task<IActionResult> Put(ClassifiedAd_RequestToPublish_V1 request)
		{
			await _classifiedAdService.Handle(request);

			return Ok();
		}
	}
}
