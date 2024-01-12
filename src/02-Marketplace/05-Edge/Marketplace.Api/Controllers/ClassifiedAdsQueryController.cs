using Framework.Application.Mediator;
using Framework.Mediator.MediatR;
using Marketplace.Queries.Contracts.ClassifiedAds.Filters;
using Marketplace.Queries.Contracts.ClassifiedAds.Results;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Marketplace.Api.Controllers;

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
	public async Task<List<ClassifiedAdItemResult>> Get([FromQuery] GetPublishedAdsQueryFilter request, CancellationToken cancellationToken)
	{
		//List<ClassifiedAdItem> result = await _mediator.Query<GetPublishedAdsQueryFilter, List<ClassifiedAdItem>>(request);

		List<ClassifiedAdItemResult> result = await _mediator.Query(request, cancellationToken);

		return result;
	}

	[HttpGet]
	[Route("myads")]
	public async Task<List<ClassifiedAdItemResult>> Get([FromQuery] GetOwnerClassifiedAdQueryFilter request, CancellationToken cancellationToken)
	{
		List<ClassifiedAdItemResult> result = await _mediator.Query(request, cancellationToken);

		return result;
	}

	[HttpGet]
	[ProducesResponseType((int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.NotFound)]
	public async Task<List<ClassifiedAdItemResult>> Get([FromQuery] GetPublicClassifiedAdQueryFilter request, CancellationToken cancellationToken)
	{
		List<ClassifiedAdItemResult> result = await _mediator.Query(request, cancellationToken);

		return result;
	}
}
