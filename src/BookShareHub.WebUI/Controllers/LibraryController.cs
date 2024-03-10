﻿using System.Security.Claims;
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

			var booksTitles = new LibraryModel
			{
				BookTitles = await _bookService.GetAllBooksAsync(userId),
			};

			return View(booksTitles);
		}
	}
}
