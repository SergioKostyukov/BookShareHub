using System.Security.Claims;
using BookShareHub.Application.Interfaces;
using BookShareHub.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShareHub.WebUI.Controllers
{
	[Authorize]
	public class PreOrderController(ILogger<PreOrderController> logger,
								 IHttpContextAccessor httpContextAccessor,
								 ILibraryService libraryService,
								 IOrderService orderService,
								 IUserService userService) : Controller
	{
		private readonly ILogger<PreOrderController> _logger = logger;
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
	}
}
