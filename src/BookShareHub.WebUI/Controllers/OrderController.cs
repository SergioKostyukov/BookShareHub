using BookShareHub.Application.Interfaces;
using BookShareHub.Application.Dto;
using BookShareHub.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookShareHub.WebUI.Controllers
{
	public class OrderController(IBookService bookService, IUserService userService) : Controller
	{
		private readonly IBookService _bookService = bookService;
		private readonly IUserService _userService = userService;

		[HttpGet]
		public async Task<IActionResult> Order(int id)
		{
			var book = await _bookService.GetBookByIdAsync(id);
			if (book == null)
			{
				return NotFound();
			}

			var ownerInfo = await _userService.GetUserByIdAsync(book.OwnerId);
			if(ownerInfo == null)
			{
				return NotFound();
			}

			var model = new OrderModel
			{
				Book = book,
				Owner = ownerInfo
			};

			return View(model);
		}
	}
}
