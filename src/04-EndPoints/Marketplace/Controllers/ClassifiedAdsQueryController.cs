using System.Net;
using Microsoft.AspNetCore.Mvc;
using Marketplace.Application.Contracts.ClassifiedAds.Queries.QueryModels;

namespace Marketplace.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClassifiedAdsQueryController : ControllerBase
{
	[HttpGet]
	[Route("list")]
	public Task<IActionResult> Get(GetPublishedClassifiedAds request)
	{
		throw new System.NotImplementedException();
	}

	[HttpGet]
	[Route("myads")]
	public Task<IActionResult> Get(GetOwnersClassifiedAd request)
	{
		throw new System.NotImplementedException();
	}

	[HttpGet]
	[ProducesResponseType((int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.NotFound)]
	public Task<IActionResult> Get(GetPublicClassifiedAd request)
	{
		throw new System.NotImplementedException();
	}
}
