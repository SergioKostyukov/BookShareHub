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
								  IRaffleService raffleService) : Controller
	{
		private readonly ILogger<RaffleController> _logger = logger;
		private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
		private readonly IBooksLibraryService _libraryService = libraryService;
		private readonly IRaffleService _raffleService = raffleService;

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
		public async Task<IActionResult> AddBookToRaffle(int orderId, int bookId)
		{

			return RedirectToAction("GetAddRaffle", new { orderId });
		}

		[HttpPost]
		public async Task<IActionResult> RemoveBookFromRaffle(int orderId, int bookId)
		{

			return RedirectToAction("GetAddRaffle", new { orderId });
		}

		[HttpPost]
		public async Task<IActionResult> DeleteRaffle(int orderId)
		{

			return RedirectToAction("MyBooksLibrary", "MyBooksLibrary");
		}

		//[HttpPost]
		//public async Task<IActionResult> AddRaffle(AddRaffleModel model)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		//		if (userId == null)
		//		{
		//			return BadRequest("UserId not found");
		//		}
		//		model.Raffle.OwnerId = userId;

		//		await _raffleService.AddRaffleAsync(model.Raffle, new ImageFileDto { ImageFile = model.ImageFile });
		//		return RedirectToAction("MyBooksLibrary", "MyBooksLibrary");
		//	}
		//	else
		//	{
		//		_logger.LogError("Error. No valid data");
		//		var errors = ModelState.Values.SelectMany(v => v.Errors);
		//		foreach (var error in errors)
		//		{
		//			_logger.LogError($"Error. {error.ErrorMessage}");
		//		}
		//	}

		//	return View("~/Views/Raffle/Raffle.cshtml", model);
		//}
	}
}
