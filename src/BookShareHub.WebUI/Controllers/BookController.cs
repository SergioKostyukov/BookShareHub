using Microsoft.AspNetCore.Mvc;

using BookShareHub.WebUI.Models;
using BookShareHub.Application.Interfaces;
using BookShareHub.Core.Domain.Entities;
using System.Security.Claims;

namespace BookShareHub.WebUI.Controllers
{
    public class BookController(IBookService bookService, IHttpContextAccessor httpContextAccessor) : Controller
    {
        private readonly IBookService _bookService = bookService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        [HttpGet]
        public IActionResult AddBook()
        {
            var model = new BookDto();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookDto modelDto)
        {
            Console.WriteLine("Start");
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

                await _bookService.AddBook(book);
                return RedirectToAction("MyBooks", "MyBooks");
            }
            else
            {
                Console.WriteLine("Not valid");
            }

            // Якщо дані не валідні, поверніть користувача на сторінку додавання книги з помилками валідації
            return View(modelDto);
        }
    }
}
