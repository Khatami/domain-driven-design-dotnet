using Marketplace.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ClassifiedAdsController : Controller
	{
		[HttpPost]
		public async Task<IActionResult> Post(ClassifiedAds.V1.Create request)
		{
			// Handle the request here

			return Ok();
		}
	}
}
