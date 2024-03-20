using System.Security.Claims;
using BookShareHub.Application.Interfaces;
using BookShareHub.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShareHub.WebUI.Controllers
{
	[Authorize]
	public class RaffleListController(ILogger<LibraryController> logger,
								  IHttpContextAccessor httpContextAccessor,
								  IRaffleService raffleService) : Controller
	{
		private readonly ILogger<LibraryController> _logger = logger;
		private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
		private readonly IRaffleService _raffleService = raffleService;

		[HttpGet]
		public async Task<IActionResult> RaffleList()
		{
			string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userId == null)
			{
				return BadRequest("UserId not found");
			}

			var model = new RafflesListModel
			{
				UserId = userId,
				RaffleTitles = await _raffleService.GetAllRafflesAsync(userId),
			};

			return View("~/Views/Active/RafflesList.cshtml", model);
		}

		[HttpGet]
		public async Task<IActionResult> Filter(RafflesListModel model)
		{
			model.RaffleTitles = await _raffleService.GetAllRafflesByFilterAsync(model.FilterQuery, model.UserId);

			return View("~/Views/Active/RafflesList.cshtml", model);
		}

		[HttpGet]
		public async Task<IActionResult> Search(RafflesListModel model)
		{
			model.RaffleTitles = await _raffleService.GetAllRafflesBySearchAsync(model.SearchQuery, model.UserId);

			return View("~/Views/Active/RafflesList.cshtml", model);
		}
	}
}
