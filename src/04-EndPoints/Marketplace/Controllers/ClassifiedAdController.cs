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
		private readonly IHandleCommand<CreateClassifiedAdCommand> _createAdCommandHandler;
		private readonly IClassifiedAdApplicationService _classifiedAdService;

		public ClassifiedAdController(IHandleCommand<CreateClassifiedAdCommand> createAdCommandHandler,
			IClassifiedAdApplicationService classifiedAdService)
		{
			_createAdCommandHandler = createAdCommandHandler;
			_classifiedAdService = classifiedAdService;
		}

		[HttpPost]
		public async Task<IActionResult> Post(CreateClassifiedAdCommand request)
		{
			// await _createAdCommandHandler.Handle(request);

			await _classifiedAdService.Handle(request);

			return Ok();
		}

		[Route("name")]
		[HttpPut]
		public async Task<IActionResult> Put(SetClassifiedAdTitleCommand request)
		{
			await _classifiedAdService.Handle(request);

			return Ok();
		}

		[Route("text")]
		[HttpPut]
		public async Task<IActionResult> Put(UpdateClassifiedAdTextCommand request)
		{
			await _classifiedAdService.Handle(request);

			return Ok();
		}

		[Route("price")]
		[HttpPut]
		public async Task<IActionResult> Put(UpdateClassifiedAdPriceCommand request)
		{
			await _classifiedAdService.Handle(request);

			return Ok();
		}

		[Route("publish")]
		[HttpPut]
		public async Task<IActionResult> Put(RequestClassifiedAdToPublishCommand request)
		{
			await _classifiedAdService.Handle(request);

			return Ok();
		}
	}
}
