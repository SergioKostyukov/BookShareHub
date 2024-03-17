using System.Security.Claims;
using BookShareHub.Application.Interfaces;
using BookShareHub.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShareHub.WebUI.Controllers
{
	[Authorize]
	public class HistoryController(ILogger<BookController> logger,
								   IHttpContextAccessor httpContextAccessor,
								   IOrderService orderService) : Controller
	{
		private readonly ILogger<BookController> _logger = logger;
		private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
		private readonly IOrderService _orderService = orderService;

		[HttpGet]
		public async Task<IActionResult> History()
		{
			string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userId == null)
			{
				return BadRequest("UserId not found");
			}

			var model = new HistoryModel
			{
				OrderTitles = await _orderService.GetDoneOrdersAsync(userId),
			};

			return View("~/Views/Contract/History.cshtml", model);
		}

		[HttpGet]
		public async Task<IActionResult> GetOrderDetails(int orderId)
		{
			var orderDetails = await _orderService.GetOrderDetailsAsync(orderId);

			return Json(orderDetails);
		}
	}
}
