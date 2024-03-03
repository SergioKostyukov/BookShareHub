using Microsoft.AspNetCore.Mvc;
using BookShareHub.Application.Interfaces;
using System.Security.Claims;

using BookShareHub.WebUI.Models;

namespace BookShareHub.WebUI.Controllers
{
	public class MyBooksController(IBookService bookService, IHttpContextAccessor httpContextAccessor) : Controller
	{
        private readonly IBookService _bookService = bookService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        [HttpGet]
        public async Task<IActionResult> MyBooks()
        {
            string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest("UserId not found");
            }
            var books = await _bookService.GetBooksByUserId(userId);
			var bookTitles = books.Select(book => new BookTitleDto
			{
				Title = book.Title,
				Author = book.Author
			}).ToList();

			return View(bookTitles);
        }
    }
}
