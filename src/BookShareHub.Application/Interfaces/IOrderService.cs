using BookShareHub.Core.Domain.Entities;
using BookShareHub.Application.Dto;

namespace BookShareHub.Application.Interfaces
{
	public interface IOrderService
	{
		Task<IEnumerable<Order>> GetOrders();

		Task<int> CreateOrder(OrderCreateDto request);

		Task DeleteOrder(int OrderId);
	}
}
