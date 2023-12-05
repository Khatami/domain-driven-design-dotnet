using Marketplace.Application.Contracts.ClassifiedAds.Queries.QueryModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Marketplace.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ClassifiedAdsQueryController : ControllerBase
	{
		[HttpGet]
		[Route("list")]
		public Task<IActionResult> Get(GetPublishedClassifiedAds request)
		{
		}

		[HttpGet]
		[Route("myads")]
		public Task<IActionResult> Get(GetOwnersClassifiedAd request)
		{
		}

		[HttpGet]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public Task<IActionResult> Get(GetPublicClassifiedAd request)
		{
		}
	}
}
