using System.Security.Claims;
using BookShareHub.Application.Interfaces;
using BookShareHub.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShareHub.WebUI.Controllers
{
	[Authorize]
	public class LibraryController(ILibraryService libraryService, IHttpContextAccessor httpContextAccessor) : Controller
	{
		private readonly ILibraryService _libraryService = libraryService;
		private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

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
				BookTitles = await _libraryService.GetAllBooksAsync(userId),
			};

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Filter(LibraryModel model)
		{
			string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userId == null)
			{
				return BadRequest("UserId not found");
			}

			model.BookTitles = await _libraryService.GetAllBooksByFilterAsync(model.FilterQuery, userId);

			return View("~/Views/Library/Library.cshtml",  model);
		}

		[HttpGet]
		public async Task<IActionResult> Search(LibraryModel model)
		{
			string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userId == null)
			{
				return BadRequest("UserId not found");
			}

			model.BookTitles = await _libraryService.GetAllBooksBySearchAsync(model.SearchQuery, userId);

			return View("~/Views/Library/Library.cshtml", model);

		}
	}
}
