using System.Security.Claims;
using BookShareHub.Application.Dto;
using BookShareHub.Application.Interfaces;
using BookShareHub.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShareHub.WebUI.Controllers
{
    [Authorize]
	public class LibraryController(IBookService bookService, IHttpContextAccessor httpContextAccessor) : Controller
	{
		private readonly IBookService _bookService = bookService;
		private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

		[HttpGet]
		public async Task<IActionResult> Library()
		{
			string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userId == null)
			{
				return BadRequest("UserId not found");
			}

			var books = await _bookService.GetAllBooksAsync(userId);
			var booksTitles = new LibraryModel
			{
				BookTitles = books.Select(book => new BookTitleDto(
					Id: book.Id,
					Title: book.Title,
					Author: book.Author,
					ImagePath: book.ImagePath
				)).ToList()
			};
			
			return View(booksTitles);
		}
	}
}
