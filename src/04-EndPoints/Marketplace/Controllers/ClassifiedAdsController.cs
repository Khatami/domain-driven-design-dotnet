using Marketplace.Application.Contracts.ClassifiedAds.V1;
using Marketplace.Application.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Controllers
{
	[Route("api/V1/[controller]")]
	[ApiController]
	public class ClassifiedAdsController : Controller
	{
		private readonly IHandleCommand<ClassifiedAd_Create_V1> _createAdCommandHandler;

		public ClassifiedAdsController(IHandleCommand<ClassifiedAd_Create_V1> createAdCommandHandler)
		{
			_createAdCommandHandler = createAdCommandHandler;
		}

		[HttpPost]
		public async Task<IActionResult> Post(ClassifiedAd_Create_V1 request)
		{
			await _createAdCommandHandler.Handle(request);

			return Ok();
		}
	}
}
