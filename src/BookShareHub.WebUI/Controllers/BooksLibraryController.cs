using System.Security.Claims;
using BookShareHub.Application.Interfaces;
using BookShareHub.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShareHub.WebUI.Controllers
{
    [Authorize]
	public class BooksLibraryController(ILogger<BooksLibraryController> logger,
								   IHttpContextAccessor httpContextAccessor,
								   IBooksLibraryService libraryService) : Controller
	{
		private readonly ILogger<BooksLibraryController> _logger = logger;
		private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
		private readonly IBooksLibraryService _libraryService = libraryService;

		[HttpGet]
		public async Task<IActionResult> BooksLibrary(int pageNumber = 1, int pageSize = 10)
		{
			_logger.LogWarning(pageNumber.ToString() + "    " + pageSize.ToString());


			string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userId == null)
			{
				return BadRequest("UserId not found");
			}

			var model = new BooksLibraryModel
			{
				UserId = userId,
				TotalItems = await _libraryService.GetTotalBooksCountAsync(userId),
				BookTitles = await _libraryService.GetBooksForPageAsync(userId, pageNumber, pageSize),
				PageNumber = pageNumber,
				PageSize = pageSize
			};

			_logger.LogWarning(model.TotalItems.ToString() + "    " + model.BookTitles.Count().ToString());

			return View("~/Views/Library/BooksLibrary.cshtml", model);
		}

		[HttpGet]
		public async Task<IActionResult> Filter(BooksLibraryModel model)
		{
			model.BookTitles = await _libraryService.GetAllBooksByFilterAsync(model.FilterQuery, model.UserId);

			return View("~/Views/Library/BooksLibrary.cshtml", model);
		}

		[HttpGet]
		public async Task<IActionResult> Search(BooksLibraryModel model)
		{
			model.BookTitles = await _libraryService.GetAllBooksBySearchAsync(model.SearchQuery, model.UserId);

			return View("~/Views/Library/BooksLibrary.cshtml", model);
		}
	}
}
