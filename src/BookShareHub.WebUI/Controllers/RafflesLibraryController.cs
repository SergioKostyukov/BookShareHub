using System.Security.Claims;
using BookShareHub.Application.Dto.Book;
using BookShareHub.Application.Dto.Order;
using BookShareHub.Application.Dto.Raffle;
using BookShareHub.Application.Interfaces;
using BookShareHub.WebUI.Models;
using BookShareHub.WebUI.Models.Raffle;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShareHub.WebUI.Controllers
{
	[Authorize]
	public class RafflesLibraryController(ILogger<BooksLibraryController> logger,
								  IHttpContextAccessor httpContextAccessor,
								  IRafflesLibraryService rafflesLibraryService,
								  IOrderService orderService) : Controller
	{
		private readonly ILogger<BooksLibraryController> _logger = logger;
		private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
		private readonly IRafflesLibraryService _rafflesLibraryService = rafflesLibraryService;
		private readonly IOrderService _orderService = orderService;

		[HttpGet]
		public async Task<IActionResult> RafflesLibrary()
		{
			string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userId == null)
			{
				return BadRequest("UserId not found");
			}

			var model = new RafflesLibraryModel
			{
				UserId = userId,
				RaffleTitles = await _rafflesLibraryService.GetAllRafflesAsync(userId),
			};

			return View("~/Views/Library/RafflesLibrary.cshtml", model);
		}

		[HttpGet]
		public async Task<IActionResult> Filter(RafflesLibraryModel model)
		{
			model.RaffleTitles = await _rafflesLibraryService.GetAllRafflesByFilterAsync(model.FilterQuery, model.UserId);

			return View("~/Views/Library/RafflesLibrary.cshtml", model);
		}

		[HttpGet]
		public async Task<IActionResult> GetEditRaffle(int id)
		{
			var model = new EditRaffleModel
			{
				Raffle = await _rafflesLibraryService.GetRaffleByIdAsync(id),
			};

			return View("~/Views/Raffle/EditRaffle.cshtml", model);
		}

		[HttpGet]
		public async Task<IActionResult> GetAddRaffle()
		{
			string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userId == null)
			{
				return BadRequest("UserId not found");
			}

			var OrderTemplateCreate = new OrderTemplateCreateDto
			{
				OwnerId = userId,
				Type = Core.Domain.Enums.OrderType.Raffle
			};

			var orderId = await _orderService.CreateOrderTemplateAsync(OrderTemplateCreate);

			return RedirectToAction("GetAddRaffle", "Raffle", new { orderId }); 
		}
	}
}
