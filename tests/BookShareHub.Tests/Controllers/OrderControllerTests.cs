using System.Security.Claims;
using BookShareHub.Application.Interfaces;
using BookShareHub.Core.Domain.Entities;
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
	public class OrderControllerTests
	{
		public readonly Mock<IHttpContextAccessor> _httpContextMock = new();
		private readonly Mock<IBooksLibraryService> _booksLibraryService = new();
		private readonly Mock<IOrderService> _orderService = new();
		private readonly Mock<IUserService> _userService = new();
		private readonly OrderDtoDataGeneration _orderDataFake = new();
		private readonly UserDtoDataGeneration _userDataFake = new();
		private readonly BookTitleDtoDataGeneration _bookDataFake = new();
		private OrderController _controller;

		[Fact]
		public async Task Order_ReturnsViewWithModel_AllServicesWorking()
		{
			// Arrange
			var expectedOrderDetails = _orderDataFake.GenerateOrder();
			var expectedUserDetails = _userDataFake.GenerateUser();
			var expectedBookTitles = _bookDataFake.GenerateBooks(3);
			_orderService.Setup(s => s.GetOrderDetailsAsync(It.IsAny<int>())).ReturnsAsync(expectedOrderDetails);
			_userService.Setup(s => s.GetUserByIdAsync(It.IsAny<string>())).ReturnsAsync(expectedUserDetails);
			_booksLibraryService.Setup(s => s.GetAllBooksByOrderIdAsync(It.IsAny<int>())).ReturnsAsync(expectedBookTitles);
			_booksLibraryService.Setup(s => s.GetAllBooksByUserIdAsync(It.IsAny<string>())).ReturnsAsync(expectedBookTitles);

			_controller = new OrderController(_httpContextMock.Object, 
						_booksLibraryService.Object, _orderService.Object, _userService.Object);

			// Act
			var result = await _controller.Order(It.IsAny<int>());

			// Assert
			var viewResult = Assert.IsType<ViewResult>(result);
			var model = Assert.IsAssignableFrom<OrderModel>(viewResult.ViewData.Model);
			Assert.Equal("~/Views/Order/Order.cshtml", viewResult.ViewName);
			Assert.Equal(expectedOrderDetails, model.Order);
			Assert.Equal(expectedUserDetails, model.Owner);
			Assert.Equal(expectedBookTitles, model.OrderList);
			Assert.Equal(expectedBookTitles, model.OtherSellerItems);
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
