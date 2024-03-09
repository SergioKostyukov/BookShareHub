using System.Security.Claims;
using BookShareHub.Application.DTOs;
using BookShareHub.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BookShareHub.Core.Domain.Enums;

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

			return View("~/Views/Book/EditBook.cshtml", modelDto);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteBook(int id)
		{
			await _bookService.DeleteBookAsync(id);
			return RedirectToAction("MyBooks", "MyBooks");
		}
	}
}


// Code to update image permanent()before save button activate)
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