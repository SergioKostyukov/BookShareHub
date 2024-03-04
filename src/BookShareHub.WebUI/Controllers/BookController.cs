using System.Security.Claims;
using BookShareHub.Application.Interfaces;
using BookShareHub.Core.Domain.Entities;
using BookShareHub.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookShareHub.WebUI.Controllers
{
	public class BookController(IBookService bookService, IHttpContextAccessor httpContextAccessor) : Controller
    {
        private readonly IBookService _bookService = bookService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

		[HttpGet]
		public async Task<IActionResult> GetEditBook(int id)
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
				Price = book.Price
			};

			return View("~/Views/Book/EditBook.cshtml", model);
		}

		[HttpGet]
		public IActionResult GetAddBook()
		{
			var model = new BookDto();
			return View("~/Views/Book/AddBook.cshtml", model);
		}

		[HttpPost]
		public async Task<IActionResult> EditBook(BookDto modelDto)
        {
			if (ModelState.IsValid)
			{
				string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				if (userId == null)
				{
					return BadRequest("UserId not found");
				}

				var book = new Book
				{
					Id = modelDto.Id,
					OwnerId = userId,
					Title = modelDto.Title,
					Author = modelDto.Author,
					Language = modelDto.Language,
					Description = modelDto.Description,
					Price = modelDto.Price
				};

				await _bookService.EditBookAsync(book);
				return RedirectToAction("MyBooks", "MyBooks");
			}

			return View(modelDto);
		}

        [HttpPost]
        public async Task<IActionResult> AddBook(BookDto modelDto)
        {
            if (ModelState.IsValid)
            {
                string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return BadRequest("UserId not found");
                }

                var book = new Book
                {
                    OwnerId = userId,
                    Title = modelDto.Title,
                    Author = modelDto.Author,
                    Language = modelDto.Language,
                    Description = modelDto.Description,
                    Price = modelDto.Price
                };

                await _bookService.AddBookAsync(book);
                return RedirectToAction("MyBooks", "MyBooks");
            }

            return View(modelDto);
        }

		[HttpPost]
		public async Task<IActionResult> DeleteBook(int id)
		{
			await _bookService.DeleteBookAsync(id);
			return RedirectToAction("MyBooks", "MyBooks");
		}
	}
}
