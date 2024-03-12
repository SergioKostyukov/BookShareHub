using System.Security.Claims;
using BookShareHub.Application.Interfaces;
using BookShareHub.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShareHub.WebUI.Controllers
{
	[Authorize]
	public class MyBooksController(ILibraryService libraryService, IHttpContextAccessor httpContextAccessor) : Controller
	{
		private readonly ILibraryService _libraryService = libraryService;
		private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

		[HttpGet]
		public async Task<IActionResult> MyBooks()
		{
			string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userId == null)
			{
				return BadRequest("UserId not found");
			}

			var booksTitles = new MyBooksModel
			{
				BookTitles = await _libraryService.GetAllBooksByUserIdAsync(userId),
			};

			return View(booksTitles);
		}

		[HttpGet]
		public async Task<IActionResult> GetEditBook(int id)
		{
			var model = new BookModel
			{
				Book = await _libraryService.GetBookByIdAsync(id),
			};

			return View("~/Views/Book/EditBook.cshtml", model);
		}

		[HttpGet]
		public IActionResult GetAddBook()
		{
			var model = new BookModel();
			return View("~/Views/Book/AddBook.cshtml", model);
		}
	}
}
