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
	public class BooksLibraryControllerTests
	{
		public readonly Mock<ILogger<BooksLibraryController>> _loggerMock = new();
		public readonly Mock<IHttpContextAccessor> _httpContextMock = new();
		private readonly Mock<IBooksLibraryService> _booksLibraryServiceMock = new();
		private readonly BookTitleDtoDataGeneration _bookDataFake = new();
		private BooksLibraryController _controller;

		[Fact]
		public async Task BooksLibrary_ReturnsViewWithModel_UserIdIsFound()
		{
			// Arrange
			var userId = "testUserId";
			_httpContextMock.Setup(c => c.HttpContext.User).Returns(HttpContextSetup(userId));

			var expectedBookTitles = _bookDataFake.GenerateBooks(3);
			var expectedBookCount = 3;
			_booksLibraryServiceMock.Setup(s => s.GetBooksForPageAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(expectedBookTitles);
			_booksLibraryServiceMock.Setup(s => s.GetTotalBooksCountAsync(It.IsAny<string>())).ReturnsAsync(expectedBookCount);
			_controller = new BooksLibraryController(_loggerMock.Object, _httpContextMock.Object, _booksLibraryServiceMock.Object);

			// Act
			var result = await _controller.BooksLibrary();

			// Assert
			var viewResult = Assert.IsType<ViewResult>(result);
			var model = Assert.IsAssignableFrom<BooksLibraryModel>(viewResult.ViewData.Model);
			Assert.Equal("~/Views/Library/BooksLibrary.cshtml", viewResult.ViewName);
			Assert.Equal(userId, model.UserId);
			Assert.Equal(expectedBookTitles, model.BookTitles);
		}

		private static ClaimsPrincipal HttpContextSetup(string userId)
		{
			var claims = new List<Claim>
			{
				new(ClaimTypes.NameIdentifier, userId)
			};
			var identity = new ClaimsIdentity(claims, "TestAuthType");
			var claimsPrincipal = new ClaimsPrincipal(identity);

			return claimsPrincipal;
		}
	}
}
