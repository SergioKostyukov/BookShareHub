using System.Security.Claims;
using BookShareHub.Application.Dto;
using BookShareHub.Application.Interfaces;
using BookShareHub.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShareHub.WebUI.Controllers
{
	[Authorize]
	public class BookController(ILogger<BookController> logger,
								IHttpContextAccessor httpContextAccessor,
								IBookService bookService) : Controller
	{
		private readonly ILogger<BookController> _logger = logger;
		private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
		private readonly IBookService _bookService = bookService;

		[HttpPost]
		public async Task<IActionResult> AddBook(AddBookModel model)
		{
			if (ModelState.IsValid)
			{
				string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				if (userId == null)
				{
					return BadRequest("UserId not found");
				}
				model.Book.OwnerId = userId;

				await _bookService.AddBookAsync(model.Book, new ImageFileDto { ImageFile = model.ImageFile });
				return RedirectToAction("MyBooksLibrary", "MyBooksLibrary");
			}
			else
			{
				_logger.LogError("Error. No valid data");
				var errors = ModelState.Values.SelectMany(v => v.Errors);
				foreach (var error in errors)
				{
					_logger.LogError($"Error. {error.ErrorMessage}");
				}
			}

			return View("~/Views/Book/AddBook.cshtml", model);
		}

		[HttpPost]
		public async Task<IActionResult> EditBook(EditBookModel model)
		{
			if (ModelState.IsValid)
			{
				await _bookService.EditBookAsync(model.Book, new ImageFileDto { ImageFile = model.ImageFile });
				return RedirectToAction("MyBooksLibrary", "MyBooksLibrary");
			}
			else
			{
				_logger.LogError("Error.No valid data");
				var errors = ModelState.Values.SelectMany(v => v.Errors);
				foreach (var error in errors)
				{
					_logger.LogError($"Error. {error.ErrorMessage}");
				}
			}

			return View("~/Views/Book/EditBook.cshtml", model);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteBook(int id)
		{
			await _bookService.DeleteBookAsync(id);
			return RedirectToAction("MyBooksLibrary", "MyBooksLibrary");
		}
	}
}


// Code to update image permanent (before save button activate)
/* BookController.cs
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
/* BookDto.cs
public class EditBookImageDto
{
	public int Id { get; set; }
	public IFormFile ImageFile { get; set; }
}
 */
/* EditBook.cshtml
@if (!string.IsNullOrEmpty(Model.ImagePath))
{
	<img src="@("~" + Model.ImagePath)" asp-append-version="true" class="img-fluid" alt="Book Image">
	<form asp-controller="Book" asp-action="ReplaceImage" method="post" enctype="multipart/form-data">
		<input type="file" name="file" id="file" class="d-none">
		<label for="file" class="btn btn-info mt-2">Replace Image</label>
	</form>
	<form asp-controller="Book" asp-action="UploadImage" method="post" enctype="multipart/form-data">
		<input type="file" name="file" id="file" class="d-none">
		<button for="file" class="btn btn-info mt-2">Upload</button>
	</form>
}
 */
/* IBookService.cs
Task EditBookImageAsync(EditBookImageDto book);
Task UploadBookImageAsync(EditBookImageDto book);
 */
/* BookService.cs
public Task EditBookImageAsync(EditBookImageDto book)
{
	throw new NotImplementedException();
}

public Task UploadBookImageAsync(EditBookImageDto book)
{
	throw new NotImplementedException();
}
 */