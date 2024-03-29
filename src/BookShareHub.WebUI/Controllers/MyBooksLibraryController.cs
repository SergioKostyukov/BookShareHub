﻿using System.Security.Claims;
using BookShareHub.Application.Dto.Book;
using BookShareHub.Application.Interfaces;
using BookShareHub.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShareHub.WebUI.Controllers
{
	[Authorize]
	public class MyBooksLibraryController(IHttpContextAccessor httpContextAccessor,
								   IBooksLibraryService libraryService) : Controller
	{
		private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
		private readonly IBooksLibraryService _libraryService = libraryService;

		[HttpGet]
		public async Task<IActionResult> MyBooksLibrary()
		{
			string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userId == null)
			{
				return BadRequest("UserId not found");
			}

			var booksTitles = new MyBooksLibraryModel
			{
				BookTitles = await _libraryService.GetAllBooksByUserIdAsync(userId),
			};

			return View("~/Views/Library/MyBooksLibrary.cshtml", booksTitles);
		}

		[HttpGet]
		public async Task<IActionResult> GetEditBook(int id)
		{
			var model = new EditBookModel
			{
				Book = await _libraryService.GetBookByIdAsync(id),
			};

			return View("~/Views/Book/EditBook.cshtml", model);
		}

		[HttpGet]
		public IActionResult GetAddBook()
		{
			return View("~/Views/Book/AddBook.cshtml", new AddBookModel { Book = new BookDto(), ImageFile = new FormFile(null, 0, 0, null, null) });
		}
	}
}
