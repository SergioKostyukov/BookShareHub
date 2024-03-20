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
		public async Task<IActionResult> BooksLibrary()
		{
			string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userId == null)
			{
				return BadRequest("UserId not found");
			}

			var model = new LibraryModel
			{
				UserId = userId,
				BookTitles = await _libraryService.GetAllBooksAsync(userId),
			};

			return View("~/Views/Library/BooksLibrary.cshtml", model);
		}

		[HttpGet]
		public async Task<IActionResult> Filter(LibraryModel model)
		{
			model.BookTitles = await _libraryService.GetAllBooksByFilterAsync(model.FilterQuery, model.UserId);

			return View("~/Views/Library/BooksLibrary.cshtml", model);
		}

		[HttpGet]
		public async Task<IActionResult> Search(LibraryModel model)
		{
			model.BookTitles = await _libraryService.GetAllBooksBySearchAsync(model.SearchQuery, model.UserId);

			return View("~/Views/Library/BooksLibrary.cshtml", model);
		}
	}
}
