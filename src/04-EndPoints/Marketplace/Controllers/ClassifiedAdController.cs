using Marketplace.Application.Contracts.ClassifiedAds.Commands.V1;
using Marketplace.Application.Contracts.ClassifiedAds.IServices;
using Marketplace.Application.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Controllers
{
	[Route("api/V1/[controller]")]
	[ApiController]
	public class ClassifiedAdController : Controller
	{
		private readonly IHandleCommand<CreateClassifiedAd> _createAdCommandHandler;
		private readonly IClassifiedAdApplicationService _classifiedAdService;

		public ClassifiedAdController(IHandleCommand<CreateClassifiedAd> createAdCommandHandler,
			IClassifiedAdApplicationService classifiedAdService)
		{
			_createAdCommandHandler = createAdCommandHandler;
			_classifiedAdService = classifiedAdService;
		}

		[HttpPost]
		public async Task<IActionResult> Post(CreateClassifiedAd request)
		{
			// await _createAdCommandHandler.Handle(request);

			await _classifiedAdService.Handle(request);

			return Ok();
		}

		[Route("name")]
		[HttpPut]
		public async Task<IActionResult> Put(SetClassifiedAdTitle request)
		{
			await _classifiedAdService.Handle(request);

			return Ok();
		}

		[Route("text")]
		[HttpPut]
		public async Task<IActionResult> Put(UpdateClassifiedAdText request)
		{
			await _classifiedAdService.Handle(request);

			return Ok();
		}

		[Route("price")]
		[HttpPut]
		public async Task<IActionResult> Put(UpdateClassifiedAdPrice request)
		{
			await _classifiedAdService.Handle(request);

			return Ok();
		}

		[Route("publish")]
		[HttpPut]
		public async Task<IActionResult> Put(RequestClassifiedAdToPublish request)
		{
			await _classifiedAdService.Handle(request);

			return Ok();
		}
	}
}
