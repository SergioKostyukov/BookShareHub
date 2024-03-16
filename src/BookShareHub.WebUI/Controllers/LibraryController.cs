using System.Security.Claims;
using BookShareHub.Application.Interfaces;
using BookShareHub.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShareHub.WebUI.Controllers
{
	[Authorize]
	public class LibraryController(ILogger<LibraryController> logger,
								   IHttpContextAccessor httpContextAccessor,
								   ILibraryService libraryService) : Controller
	{
		private readonly ILogger<LibraryController> _logger = logger;
		private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
		private readonly ILibraryService _libraryService = libraryService;

		[HttpGet]
		public async Task<IActionResult> Library()
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

			return View("~/Views/Library/Library.cshtml", model);
		}

		[HttpGet]
		public async Task<IActionResult> Filter(LibraryModel model)
		{
			model.BookTitles = await _libraryService.GetAllBooksByFilterAsync(model.FilterQuery, model.UserId);

			return View("~/Views/Library/Library.cshtml", model);
		}

		[HttpGet]
		public async Task<IActionResult> Search(LibraryModel model)
		{
			model.BookTitles = await _libraryService.GetAllBooksBySearchAsync(model.SearchQuery, model.UserId);

			return View("~/Views/Library/Library.cshtml", model);
		}
	}
}
