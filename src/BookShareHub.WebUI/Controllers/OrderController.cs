using System.Security.Claims;
using BookShareHub.Application.Interfaces;
using BookShareHub.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShareHub.WebUI.Controllers
{
	[Authorize]
	public class OrderController(ILogger<OrderController> logger,
								 IHttpContextAccessor httpContextAccessor,
								 ILibraryService libraryService,
								 IOrderService orderService,
								 IUserService userService) : Controller
	{
		private readonly ILogger<OrderController> _logger = logger;
		private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
		private readonly ILibraryService _libraryService = libraryService;
		private readonly IOrderService _orderService = orderService;
		private readonly IUserService _userService = userService;

		[HttpGet]
		public async Task<IActionResult> PreOrder(int id)
		{
			var book = await _libraryService.GetBookByIdAsync(id);
			if (book == null)
			{
				return NotFound();
			}

			var ownerInfo = await _userService.GetUserByIdAsync(book.OwnerId);
			if (ownerInfo == null)
			{
				return NotFound();
			}

			var model = new PreOrderModel
			{
				Book = book,
				Owner = ownerInfo
			};

			return View("~/Views/Order/PreOrder.cshtml", model);
		}

		[HttpGet]
		public async Task<IActionResult> Order(int orderId)
		{
			var orderDetails = await _orderService.GetOrderDetailsAsync(orderId);
			if (orderDetails == null)
			{
				return NotFound();
			}

			var ownerInfo = await _userService.GetUserByIdAsync(orderDetails.OwnerId);
			if (ownerInfo == null)
			{
				return NotFound();
			}

			var model = new OrderModel
			{
				Order = orderDetails,
				Owner = ownerInfo,
				OrderList = await _libraryService.GetAllBooksByOrderIdAsync(orderId),
				OtherSellerItems = await _libraryService.GetAllBooksByUserIdAsync(orderDetails.OwnerId)
			};

			return View("~/Views/Order/Order.cshtml", model);
		}

		[HttpGet]
		public async Task<IActionResult> ConfirmedOrder(int orderId)
		{
			string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userId == null)
			{
				return BadRequest("UserId not found");
			}

			var orderDetails = await _orderService.GetOrderDetailsAsync(orderId);
			if (orderDetails == null)
			{
				return NotFound();
			}

			var ownerInfo = await _userService.GetUserByIdAsync(orderDetails.OwnerId);
			if (ownerInfo == null)
			{
				return NotFound();
			}

			var model = new ConfirmedOrderModel
			{
				Order = orderDetails,
				User = ownerInfo,
				OrderList = await _libraryService.GetAllBooksByOrderIdAsync(orderId),
				UserId = userId
			};

			return View("~/Views/Order/ConfirmedOrder.cshtml", model);
		}

		[HttpPost]
		public async Task<IActionResult> AddOrder(PreOrderModel model)
		{
			string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userId == null)
			{
				return BadRequest("UserId not found");
			}

			var OrderCreate = new Application.Dto.Order.OrderCreateDto
			(
				CustomerId: userId,
				OwnerId: model.Owner.Id,
				BookId: model.Book.Id,
				Type: Core.Domain.Enums.OrderType.Sale,
				CheckAmount: model.Book.Price
			);

			var orderId = await _orderService.CreateOrderAsync(OrderCreate);

			return RedirectToAction("Order", "Order", new { orderId });
		}

		[HttpPost]
		public async Task<IActionResult> ConfirmOrder(OrderModel model)
		{
			var OrderConfirm = new Application.Dto.Order.OrderConfirmDto
			(
				OrderId: model.Order.Id,
				OwnerId: model.Owner.Id,
				OwnerName: model.Owner.UserName
				// Other order parameters(delivery, pay options)
			);

			await _orderService.ConfirmOrderAsync(OrderConfirm);

			return RedirectToAction("Library", "Library");
		}

		[HttpPost]
		public async Task<IActionResult> DeleteOrder(int id)
		{
			await _orderService.DeleteOrderAsync(id);

			return RedirectToAction("Library", "Library");
		}

		[HttpPost]
		public async Task<IActionResult> DeleteBookFromOrder(OrderModel model)
		{
			var isLast = await _orderService.DeleteBookFromOrderAsync(model.DeleteBookDetails);

			if (isLast)
			{
				return RedirectToAction("Library", "Library");
			}
			else
			{
				return RedirectToAction("Order", "Order", new { model.DeleteBookDetails.OrderId });
			}
		}
	}
}
