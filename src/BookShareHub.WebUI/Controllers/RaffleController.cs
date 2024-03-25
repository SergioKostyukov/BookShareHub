using System.Security.Claims;
using BookShareHub.Application.Dto.Raffle;
using BookShareHub.Application.Interfaces;
using BookShareHub.Core.Domain.Entities;
using BookShareHub.WebUI.Models;
using MailKit.Search;
using Microsoft.AspNetCore.Mvc;

namespace BookShareHub.WebUI.Controllers
{
	public class RaffleController(ILogger<RaffleController> logger,
								  IHttpContextAccessor httpContextAccessor,
								  IBooksLibraryService booksLibraryService,
								  IRafflesLibraryService rafflesLibraryService,
								  IRaffleService raffleService,
								  IOrderService orderService,
								  IUserService userService) : Controller
	{
		private readonly ILogger<RaffleController> _logger = logger;
		private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
		private readonly IBooksLibraryService _booksLibraryService = booksLibraryService;
		private readonly IRafflesLibraryService _rafflesLibraryService = rafflesLibraryService;
		private readonly IRaffleService _raffleService = raffleService;
		private readonly IOrderService _orderService = orderService;
		private readonly IUserService _userService = userService;

		[HttpGet]
		public async Task<IActionResult> Raffle(int raffleId)
		{
			var raffle = await _rafflesLibraryService.GetRaffleByIdAsync(raffleId);
            if (raffle == null)
			{
				return NotFound();
			}

			var ownerInfo = await _userService.GetUserByIdAsync(raffle.OwnerId);
			if (ownerInfo == null)
			{
				return NotFound();
			}

			var model = new RaffleModel
			{
				Raffle = raffle,
				Owner = ownerInfo,
				OrderDetails = await _orderService.GetOrderDetailsAsync(raffle.OrderId),
				RaffleList = await _booksLibraryService.GetAllBooksByOrderIdAsync(raffle.OrderId)
			};

			return View("~/Views/Raffle/Raffle.cshtml", model);
		}

		[HttpGet]
		public async Task<IActionResult> GetAddRaffle(int orderId)
		{
			string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userId == null)
			{
				return BadRequest("UserId not found");
			}

			var model = new AddRaffleModel
			{
				OwnerId = userId,
				OrderId = orderId,
				RaffleList = await _booksLibraryService.GetAllBooksByOrderIdAsync(orderId),
				MyOtherBooks = await _booksLibraryService.GetAllBooksByUserIdAsync(userId)
			};

			return View("~/Views/Raffle/AddRaffle.cshtml", model);
		}

		[HttpGet]
		public async Task<IActionResult> GetEditRaffle(int raffleId)
		{
			//var model = new EditRaffleModel
			//{
			//	Raffle = await _rafflesLibraryService.GetRaffleByIdAsync(raffleId),
			//};

			return View("~/Views/Raffle/EditRaffle.cshtml");
		}

		[HttpPost]
		public async Task<IActionResult> AddRaffle(AddRaffleModel model)
		{
			if (ModelState.IsValid)
			{
				await _raffleService.AddRaffleAsync(model.RaffleCreate);

				return RedirectToAction("RafflesLibrary", "RafflesLibrary");
			}
			else
			{
				_logger.LogError("Error. No valid data");
				var errors = ModelState.Values.SelectMany(v => v.Errors);
				foreach (var error in errors)
				{
					_logger.LogError($"Error. {error.ErrorMessage}");
				}
			}

			return View("~/Views/Raffle/AddRaffle.cshtml", model);
		}

		[HttpPost]
		public async Task<IActionResult> AddBookToRaffle(AddRaffleModel model)
		{
			await _orderService.AddBookToOrderAsync(model.BookActionDetails);

			return RedirectToAction("GetAddRaffle", new { orderId = model.BookActionDetails.OrderId });
		}

		[HttpPost]
		public async Task<IActionResult> RemoveBookFromRaffle(AddRaffleModel model)
		{
			await _orderService.DeleteBookFromOrderAsync(model.BookActionDetails);

			return RedirectToAction("GetAddRaffle", new { orderId = model.BookActionDetails.OrderId });
		}

		[HttpPost]
		public async Task<IActionResult> DeleteOrder(int orderId)
		{
			await _orderService.DeleteOrderAsync(orderId);

			return RedirectToAction("RafflesLibrary", "RafflesLibrary");
		}
	}
}
