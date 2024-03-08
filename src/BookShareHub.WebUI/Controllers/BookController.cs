using System.Security.Claims;
using BookShareHub.Application.DTOs;
using BookShareHub.Application.Interfaces;
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
				OwnerId = book.OwnerId,
				Title = book.Title,
				Author = book.Author,
				Language = book.Language,
				Description = book.Description,
				Price = book.Price,
				ImagePath = book.ImagePath
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
		public async Task<IActionResult> AddBook(BookDto modelDto)
		{
			if (ModelState.IsValid)
			{
				string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				if (userId == null)
				{
					return BadRequest("UserId not found");
				}
				modelDto.OwnerId = userId;

				await _bookService.AddBookAsync(modelDto);
				return RedirectToAction("MyBooks", "MyBooks");
			}

			return View(modelDto);
		}

		[HttpPost]
		public async Task<IActionResult> EditBook(BookDto modelDto)
		{
			if (ModelState.IsValid)
			{
				await _bookService.EditBookAsync(modelDto);
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


/* methods to update image permanent (defore save button activate)
 
		[HttpPost]
		public async Task<IActionResult> ReplaceBook(BookDto model)
		{
			

			return View("~/Views/Book/EditBook.cshtml", model);
		}

		[HttpPost]
		public async Task<IActionResult> UploadBook(BookDto model)
		{
			//string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			//if (userId == null)
			//{
			//	return BadRequest("UserId not found");
			//}

			//var uploadBookInfo = new UploadBookImageDto
			//{
			//	Id = userId,
			//	ImagePath = model.ImageFile.Name
			//};

			//await _bookService.UploadBookImageAsync(uploadBookInfo);

			return View("~/Views/Book/EditBook.cshtml", model);
		}
 
 */