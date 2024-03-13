using BookShareHub.Application.Dto;

namespace BookShareHub.Application.Interfaces
{
	public interface IOrderService
	{
		Task<List<ActualOrderTitleDto>> GetActualOrdersAsync(string userId);
		Task<List<DoneOrderTitleDto>> GetDoneOrdersAsync(string userId);
		Task<OrderDto> GetOrderDetailsAsync(int orderId);

		Task<int> CreateOrderAsync(OrderCreateDto request);

		Task DeleteOrderAsync(int OrderId);
	}
}
