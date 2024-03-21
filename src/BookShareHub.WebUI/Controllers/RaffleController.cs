using System.Security.Claims;
using BookShareHub.Application.Dto.Raffle;
using BookShareHub.Application.Interfaces;
using BookShareHub.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookShareHub.WebUI.Controllers
{
	public class RaffleController(ILogger<RaffleController> logger,
								  IHttpContextAccessor httpContextAccessor,
								  IBooksLibraryService libraryService,
								  IRaffleService raffleService,
								  IOrderService orderService) : Controller
	{
		private readonly ILogger<RaffleController> _logger = logger;
		private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
		private readonly IBooksLibraryService _libraryService = libraryService;
		private readonly IRaffleService _raffleService = raffleService;
		private readonly IOrderService _orderService = orderService;

		[HttpGet]
		public async Task<IActionResult> GetAddRaffle(int orderId)
		{
			_logger.LogWarning("Raffle controller. " + orderId.ToString());
			string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userId == null)
			{
				return BadRequest("UserId not found");
			}

			var model = new AddRaffleModel
			{
				OwnerId = userId,
				OrderId = orderId,
				RaffleList = await _libraryService.GetAllBooksByOrderIdAsync(orderId),
				MyOtherBooks = await _libraryService.GetAllBooksByUserIdAsync(userId)
			};

			return View("~/Views/Raffle/AddRaffle.cshtml", model);
		}

		[HttpPost]
		public async Task<IActionResult> AddRaffle(AddRaffleModel model)
		{
			if (ModelState.IsValid)
			{
				_logger.LogWarning(model.OrderId.ToString());

				var RaffleCreate = new RaffleCreateDto(
					OwnerId: model.OwnerId,
					OrderId: model.OrderId,
					Type: model.Type,
					TicketPrice: model.TicketPrice,
					EndDateTime: model.EndDateTime,
					Description: model.Description
				);

				await _raffleService.AddRaffleAsync(RaffleCreate);

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
			_logger.LogWarning("Add to raffle started");
			await _orderService.AddBookToOrder(model.DeleteBookDetails.OrderId, model.DeleteBookDetails.Id);
			_logger.LogWarning("Add to raffle ended");

			return RedirectToAction("GetAddRaffle", new { orderId = model.DeleteBookDetails.OrderId });
		}

		[HttpPost]
		public async Task<IActionResult> RemoveBookFromRaffle(AddRaffleModel model)
		{
			_logger.LogWarning("Delete form raffle started");

			await _orderService.DeleteBookFromRaffleOrderAsync(model.DeleteBookDetails);

			_logger.LogWarning("Delete form raffle finished");

			return RedirectToAction("GetAddRaffle", new { orderId = model.DeleteBookDetails.OrderId });
		}

		[HttpPost]
		public async Task<IActionResult> DeleteOrder(int orderId)
		{
			await _orderService.DeleteOrderAsync(orderId);

			return RedirectToAction("RafflesLibrary", "RafflesLibrary");
		}
	}
}
