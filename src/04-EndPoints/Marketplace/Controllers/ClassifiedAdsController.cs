using Marketplace.Application.ClassifiedAds.Services;
using Marketplace.Application.Contracts.ClassifiedAds.V1;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Controllers
{
	[Route("api/V1/[controller]")]
	[ApiController]
	public class ClassifiedAdsController : Controller
	{
		private readonly ClassifiedAdsApplicationService _classifiedAdsApplicationService;

		public ClassifiedAdsController(ClassifiedAdsApplicationService classifiedAdsApplicationService)
		{
			_classifiedAdsApplicationService = classifiedAdsApplicationService;
		}

		[HttpPost]
		public async Task<IActionResult> Post(ClassifiedAd_Create_V1 request)
		{
			_classifiedAdsApplicationService.Handle(request);

			return Ok();
		}
	}
}
