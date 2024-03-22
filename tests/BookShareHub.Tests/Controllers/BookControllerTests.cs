using System.Security.Claims;
using System.Text;
using BookShareHub.Application.Dto;
using BookShareHub.Application.Dto.Book;
using BookShareHub.Application.Interfaces;
using BookShareHub.Tests.DataGeneration;
using BookShareHub.WebUI.Controllers;
using BookShareHub.WebUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BookShareHub.Tests.Controllers
{
	public class BookControllerTests
	{
		public readonly Mock<ILogger<BookController>> _loggerMock = new();
		public readonly Mock<IHttpContextAccessor> _httpContextMock = new();
		public readonly Mock<IBookService> _bookServiceMock = new();
		private readonly BookDtoDataGeneration _bookDataFake = new();
		private BookController _controller;

		[Fact]
		public async Task AddBook_ReturnsRedirectToAction_ValidModelState()
		{
			// Arrange
			_httpContextMock.Setup(c => c.HttpContext.User).Returns(HttpContextSetup());
			_bookServiceMock.Setup(c => c.AddBookAsync(It.IsAny<BookDto>(), It.IsAny<ImageFileDto>())).Returns(Task.CompletedTask);
			_controller = new BookController(_loggerMock.Object, _httpContextMock.Object, _bookServiceMock.Object);

			var book = _bookDataFake.GenerateBook();
			var image = CreateTestFormFile();
			var model = new AddBookModel { Book = book, ImageFile = image };

			// Act
			var result = await _controller.AddBook(model);

			// Assert
			var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal("MyBooksLibrary", redirectToActionResult.ActionName);
			Assert.Equal("MyBooksLibrary", redirectToActionResult.ControllerName);
			_bookServiceMock.Verify(x => x.AddBookAsync(It.IsAny<BookDto>(), It.IsAny<ImageFileDto>()), Times.Once);
		}

		[Fact]
		public async Task AddBook_ReturnsViewResult_InvalidModelState()
		{
			// Arrange
			_httpContextMock.Setup(c => c.HttpContext.User).Returns(HttpContextSetup());
			_controller = new BookController(_loggerMock.Object, _httpContextMock.Object, _bookServiceMock.Object);
			_controller.ModelState.AddModelError("Book.Title", "The Title field is required");

			var book = _bookDataFake.GenerateBook();
			var image = CreateTestFormFile();
			var model = new AddBookModel { Book = book, ImageFile = image };

			// Act
			var result = await _controller.AddBook(model);

			// Assert
			var viewResult = Assert.IsType<ViewResult>(result);
			Assert.Equal("~/Views/Book/AddBook.cshtml", viewResult.ViewName);
			_bookServiceMock.Verify(x => x.AddBookAsync(It.IsAny<BookDto>(), It.IsAny<ImageFileDto>()), Times.Never);
		}

		[Fact]
		public async Task AddBook_ReturnsBadRequest_NoUserInHttpContest()
		{
			// Arrange
			_controller = new BookController(_loggerMock.Object, _httpContextMock.Object, _bookServiceMock.Object);

			var book = _bookDataFake.GenerateBook();
			var image = CreateTestFormFile();
			var model = new AddBookModel { Book = book, ImageFile = image };

			// Act
			var result = await _controller.AddBook(model);

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
			Assert.Equal("UserId not found", badRequestResult.Value);
			_bookServiceMock.Verify(x => x.AddBookAsync(It.IsAny<BookDto>(), It.IsAny<ImageFileDto>()), Times.Never);
		}

		[Fact]
		public async Task EditBook_ReturnsRedirectToActionResult_ValidModelState()
		{
			// Arrange
			_controller = new BookController(_loggerMock.Object, _httpContextMock.Object, _bookServiceMock.Object);

			var book = _bookDataFake.GenerateBook();
			var image = CreateTestFormFile();
			var model = new EditBookModel { Book = book, ImageFile = image };

			// Act
			var result = await _controller.EditBook(model);

			// Assert
			var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal("MyBooksLibrary", redirectToActionResult.ActionName);
			Assert.Equal("MyBooksLibrary", redirectToActionResult.ControllerName);
			_bookServiceMock.Verify(x => x.EditBookAsync(It.IsAny<BookDto>(), It.IsAny<ImageFileDto>()), Times.Once);
		}

		[Fact]
		public async Task EditBook_ReturnsViewResult_InvalidModelState()
		{
			// Arrange
			_controller = new BookController(_loggerMock.Object, _httpContextMock.Object, _bookServiceMock.Object);

			var book = _bookDataFake.GenerateBook();
			var image = CreateTestFormFile();
			var model = new EditBookModel { Book = book, ImageFile = image };

			_controller.ModelState.AddModelError("Book.Title", "The Title field is required");

			// Act
			var result = await _controller.EditBook(model);

			// Assert
			var viewResult = Assert.IsType<ViewResult>(result);
			Assert.Equal("~/Views/Book/EditBook.cshtml", viewResult.ViewName);
			_bookServiceMock.Verify(x => x.EditBookAsync(It.IsAny<BookDto>(), It.IsAny<ImageFileDto>()), Times.Never); 
		}

		[Fact]
		public async Task DeleteBook_ReturnsRedirectToActionResult_ValidUserId()
		{
			// Arrange
			_httpContextMock.Setup(c => c.HttpContext.User).Returns(HttpContextSetup());
			_controller = new BookController(_loggerMock.Object, _httpContextMock.Object, _bookServiceMock.Object);

			var random = new Random();
			var bookId = random.Next();

			// Act
			var result = await _controller.DeleteBook(bookId);

			// Assert
			var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal("MyBooksLibrary", redirectToActionResult.ActionName);
			Assert.Equal("MyBooksLibrary", redirectToActionResult.ControllerName);
			_bookServiceMock.Verify(x => x.DeleteBookAsync(It.IsAny<int>()), Times.Once);
		}

		private static FormFile CreateTestFormFile()
		{
			var content = "Test file content";
			var fileName = "test.jpg";
			var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
			return new FormFile(stream, 0, content.Length, null, fileName)
			{
				Headers = new HeaderDictionary(),
				ContentType = "image/jpeg"
			};
		}

		private static ClaimsPrincipal HttpContextSetup()
		{
			var fakeUserId = Guid.NewGuid().ToString();
			var claims = new List<Claim>
			{
				new(ClaimTypes.NameIdentifier, fakeUserId)
			};
			var identity = new ClaimsIdentity(claims, "TestAuthType");
			var claimsPrincipal = new ClaimsPrincipal(identity);

			return claimsPrincipal;
		}
	}
}
