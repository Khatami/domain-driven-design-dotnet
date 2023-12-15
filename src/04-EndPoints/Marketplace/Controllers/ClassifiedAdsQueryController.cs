using Marketplace.Application.Infrastructure.Mediator;
using Marketplace.Queries.Contracts.ClassifiedAds.QueryFilters;
using Marketplace.Queries.Contracts.ClassifiedAds.QueryResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Marketplace.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
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
		//List<ClassifiedAdItem> result = await _mediator.Query<GetPublishedAdsQueryFilter, List<ClassifiedAdItem>>(request);

		List<ClassifiedAdItem> result = await _mediator.Query(request);

		return result;
	}

	[HttpGet]
	[Route("myads")]
	public async Task<List<ClassifiedAdItem>> Get([FromQuery] GetOwnerClassifiedAdQueryFilter request)
	{
		List<ClassifiedAdItem> result = await _mediator.Query(request);

		return result;
	}

	[HttpGet]
	[ProducesResponseType((int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.NotFound)]
	public async Task<List<ClassifiedAdItem>> Get([FromQuery] GetPublicClassifiedAdQueryFilter request)
	{
		List<ClassifiedAdItem> result = await _mediator.Query(request);

		return result;
	}
}
