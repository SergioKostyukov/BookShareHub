using System.Security.Claims;
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
	public class RafflesLibraryControllerTests
	{
		public readonly Mock<ILogger<RafflesLibraryController>> _loggerMock = new();
		public readonly Mock<IHttpContextAccessor> _httpContextMock = new();
		private readonly Mock<IRafflesLibraryService> _rafflesLibraryService = new();
		private readonly Mock<IOrderService> _orderService = new();
		private readonly RaffleTitleDtoDataGeneration _bookDataFake = new();
		private RafflesLibraryController _controller;

		[Fact]
		public async Task RafflesLibrary_ReturnsViewWithModel_UserIdIsFound()
		{
			// Arrange
			var userId = "testUserId";
			_httpContextMock.Setup(c => c.HttpContext.User).Returns(HttpContextSetup(userId));

			var expectedBookTitles = _bookDataFake.GenerateBooks(3);
			_rafflesLibraryService.Setup(s => s.GetAllRafflesAsync(It.IsAny<string>())).ReturnsAsync(expectedBookTitles);
			_controller = new RafflesLibraryController(_loggerMock.Object, _httpContextMock.Object, _rafflesLibraryService.Object, _orderService.Object);

			// Act
			var result = await _controller.RafflesLibrary();

			// Assert
			var viewResult = Assert.IsType<ViewResult>(result);
			var model = Assert.IsAssignableFrom<RafflesLibraryModel>(viewResult.ViewData.Model);
			Assert.Equal("~/Views/Library/RafflesLibrary.cshtml", viewResult.ViewName);
			Assert.Equal(userId, model.UserId);
			Assert.Equal(expectedBookTitles, model.RaffleTitles);
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
