using BookShareHub.Application.Interfaces;
using BookShareHub.WebUI.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace BookShareHub.WebUI.Controllers
{
	public class HistoryController(ILogger<BookController> logger, IOrderService orderService, IHttpContextAccessor httpContextAccessor) : Controller
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

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> GetOrderDetails(int orderId)
		{
			var orderDetails = await _orderService.GetOrderDetailsAsync(orderId);

			_logger.LogInformation("{0} {1} {2}", orderDetails.OwnerId, orderDetails.CustomerId, orderDetails.CheckAmount);

			return Json(orderDetails);
		}
	}
}
