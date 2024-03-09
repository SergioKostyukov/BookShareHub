using BookShareHub.Application.Interfaces;
using BookShareHub.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookShareHub.WebUI.Controllers
{
	public class OrderController(IBookService bookService, IHttpContextAccessor httpContextAccessor) : Controller
	{
		private readonly IBookService _bookService = bookService;
		private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

		[HttpGet]
		public async Task<IActionResult> Order(int id)
		{
			var book = await _bookService.GetBookByIdAsync(id);
			if (book == null)
			{
				return NotFound();
			}

			var model = new BookDto
			{
				Id = book.Id,
				Title = book.Title,
				Author = book.Author,
				Language = book.Language,
				Description = book.Description,
				Price = book.Price,
				ImagePath = book.ImagePath,
			};

			return View(model);
		}
	}
}
