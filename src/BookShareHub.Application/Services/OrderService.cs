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

		/* ----------------------- GET METHODS ----------------------- */
		// Get all orders that were not fulfilled or canceled
		public async Task<List<ActualOrderTitleDto>> GetActualOrdersAsync(string userId)
		{
			var orders = await _context.Orders
				.Where(b => b.CustomerId == userId &&
							b.Status != Core.Domain.Enums.OrderStatus.Done)
				.ToListAsync();

			return _mapper.Map<List<ActualOrderTitleDto>>(orders);
		}

		// Get all orders that were fulfilled or canceled
		public async Task<List<DoneOrderTitleDto>> GetDoneOrdersAsync(string userId)
		{
			var orders = await _context.Orders
				.Where(b => b.CustomerId == userId &&
							b.Status == Core.Domain.Enums.OrderStatus.Done)
				.ToListAsync();

			return _mapper.Map<List<DoneOrderTitleDto>>(orders);
		}

		public async Task<OrderDto> GetOrderDetailsAsync(int orderId)
		{
			var order = await _context.Orders
				.FirstOrDefaultAsync(b => b.Id == orderId);

			return _mapper.Map<OrderDto>(order);
		}

		/* ----------------------- PATCH METHODS ----------------------- */
		// Create a new basket with seller`s items or get an existing one and add selected book to it
		public async Task<int> CreateOrderAsync(OrderCreateDto request)
		{
			var id = await _context.Orders
				.Where(ol => ol.CustomerId == request.CustomerId && ol.OwnerId == request.OwnerId)
				.Select(ol => ol.Id)
				.FirstOrDefaultAsync();

			// If there is no such basket yet
			if (id == 0)
			{
				id = await CreateOrderRecordAsync(request);
			}

			// Adding book to basket
			await CreateOrderListRecordAsync(request, id);

			return id;
		}

		// Remove all records from the 'Orders' and 'OrderLists' tables
		public async Task DeleteOrderAsync(int orderId)
		{
			var order = await _context.Orders
				.Where(o => o.Id == orderId)
				.FirstOrDefaultAsync() ?? throw new InvalidOperationException("Order not found");

			var orderLists = _context.OrdersLists.Where(ol => ol.OrderId == order.Id);

			_context.OrdersLists.RemoveRange(orderLists);
			_logger.LogInformation("OrderList cleared");

			_context.Orders.Remove(order);
			_logger.LogInformation("Order deleted");

			await _context.SaveChangesAsync();
		}

		/* ----------------------- PRIVATE METHODS ----------------------- */
		// Create new record in 'Orders' table
		private async Task<int> CreateOrderRecordAsync(OrderCreateDto request)
		{
			var order = _mapper.Map<Order>(request);
			order.Status = Core.Domain.Enums.OrderStatus.Template;
			order.CreateDate = DateTime.Now;
			order.CheckAmount = 0;

			_context.Orders.Add(order);
			await _context.SaveChangesAsync();

			return order.Id;
		}

		// Create new record in 'OrdersLists' table. Add selected book to order list
		private async Task CreateOrderListRecordAsync(OrderCreateDto request, int orderId)
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
