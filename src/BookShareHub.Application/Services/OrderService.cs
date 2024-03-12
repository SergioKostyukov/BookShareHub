using BookShareHub.Application.Interfaces;
using BookShareHub.Core.Domain.Entities;
using BookShareHub.Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace BookShareHub.Application.Services
{
	internal class OrderService(ILogger<OrderService> logger, BookShareHubDbContext context) : IOrderService
	{
		private readonly ILogger<OrderService> _logger = logger;
		private readonly BookShareHubDbContext _context = context;

		public Task CreateOrder()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Order> GetOrders()
		{
			throw new NotImplementedException();
		}

		// ----------------------- GET METHODS -----------------------


		// ----------------------- PATCH METHODS -----------------------


	}
}
