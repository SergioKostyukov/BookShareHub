using System.Security.Claims;
using BookShareHub.Application.Interfaces;
using BookShareHub.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookShareHub.WebUI.Controllers
{
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
			var model = new OrderModel
			{
				Id = orderId,
			};

			return View("~/Views/Order/Order.cshtml", model);
		}

		[HttpGet]
		public async Task<IActionResult> ConfirmOrder()
		{


			return RedirectToAction("Library", "Library");
		}

		[HttpPost]
		public async Task<IActionResult> AddOrder([FromForm] string ownerId, [FromForm] int bookId, [FromForm] decimal checkAmount)
		{
			string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userId == null)
			{
				return BadRequest("UserId not found");
			}

			var OrderCreate = new Application.Dto.OrderCreateDto
			(
				CustomerId: userId,
				OwnerId: ownerId,
				BookId: bookId,
				Type: Core.Domain.Enums.OrderType.Sale,
				CheckAmount: checkAmount
			);

			var orderId = await _orderService.CreateOrderAsync(OrderCreate);

			return RedirectToAction("Order", "Order", new { orderId });
		}

		[HttpPost]
		public async Task<IActionResult> DeleteOrder(int id)
		{
			await _orderService.DeleteOrderAsync(id);

			return RedirectToAction("Library", "Library");
		}
	}
}
