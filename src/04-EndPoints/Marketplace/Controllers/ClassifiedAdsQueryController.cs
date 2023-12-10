using Marketplace.Application.Infrastructure.Mediator;
using Marketplace.Query.ClassifiedAd.Models;
using Marketplace.Query.ClassifiedAd.QueryFilters;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClassifiedAdsQueryController : ControllerBase
{
	private readonly IApplicationMediator _mediator;

	public ClassifiedAdsQueryController(IApplicationMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet]
	[Route("list")]
	public async Task<List<ClassifiedAdItem>> Get([FromQuery] GetPublishedAdsQueryFilter request)
	{
		List<ClassifiedAdItem> result = await _mediator.Query<GetPublishedAdsQueryFilter, List<ClassifiedAdItem>>(request);

		return result;
	}

	//[HttpGet]
	//[Route("myads")]
	//public Task<IActionResult> Get(GetOwnersClassifiedAdQuery request)
	//{
	//	throw new System.NotImplementedException();
	//}

	//[HttpGet]
	//[ProducesResponseType((int)HttpStatusCode.OK)]
	//[ProducesResponseType((int)HttpStatusCode.NotFound)]
	//public Task<IActionResult> Get(GetPublicClassifiedAdQuery request)
	//{
	//	throw new System.NotImplementedException();
	//}
}
