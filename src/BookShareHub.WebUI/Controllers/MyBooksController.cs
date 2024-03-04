﻿using System.Security.Claims;
using BookShareHub.Application.Interfaces;
using BookShareHub.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

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
                Id = book.Id,
				Title = book.Title,
				Author = book.Author
			}).ToList();

			return View(bookTitles);
        }
    }
}
