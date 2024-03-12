using AutoMapper;
using BookShareHub.Application.Dto;
using BookShareHub.Application.Interfaces;
using BookShareHub.Core.Domain.Entities;
using BookShareHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookShareHub.Application.Services
{
	internal class OrderService(ILogger<OrderService> logger, BookShareHubDbContext context, IMapper mapper) : IOrderService
	{
		private readonly ILogger<OrderService> _logger = logger;
		private readonly BookShareHubDbContext _context = context;
		private readonly IMapper _mapper = mapper;

		// ----------------------- GET METHODS -----------------------
		async public Task<IEnumerable<Order>> GetOrders()
		{
			throw new NotImplementedException();
		}

		// ----------------------- PATCH METHODS -----------------------
		async public Task<int> CreateOrder(OrderCreateDto request)
		{
			var id = await _context.Orders
				.Where(ol => ol.CustomerId == request.CustomerId && ol.OwnerId == request.OwnerId)
				.Select(ol => ol.Id)
				.FirstOrDefaultAsync();

			// If there is no such basket yet
			if (id == 0)
			{
				id = await CreateOrderRecord(request);
			}

			// Adding book to basket
			await CreateOrderListRecord(request, id);

			return id;
		}

		async public Task DeleteOrder(int OrderId)
		{

		}

		async private Task<int> CreateOrderRecord(OrderCreateDto request)
		{
			var order = _mapper.Map<Order>(request);
			order.Status = Core.Domain.Enums.OrderStatus.Request;
			order.CreateDate = DateTime.Now;
			order.CheckAmount = 0;

			_context.Orders.Add(order);
			await _context.SaveChangesAsync();

			return order.Id;
		}

		async private Task CreateOrderListRecord(OrderCreateDto request, int orderId)
		{
			// If there is no such book in basket yet
			var existingOrderList = await _context.OrdersLists
				.FirstOrDefaultAsync(ol => ol.BookId == request.BookId && ol.OrderId == orderId);

			if (existingOrderList == null)
			{
				var bookQuery = new OrderList
				{
					BookId = request.BookId,
					OrderId = orderId,
				};

				_context.OrdersLists.Add(bookQuery);
				await _context.SaveChangesAsync();

				// Add book price to order amount check
				var order = await _context.Orders.FindAsync(orderId);
				if (order != null)
				{
					order.CheckAmount += request.CheckAmount;
					await _context.SaveChangesAsync();
				}
			}
		}
	}
}
