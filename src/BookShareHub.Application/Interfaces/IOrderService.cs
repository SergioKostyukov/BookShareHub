using BookShareHub.Application.Dto.Book;
using BookShareHub.Application.Dto.Order;

namespace BookShareHub.Application.Interfaces
{
    public interface IOrderService
	{
		Task<List<ActualOrderTitleDto>> GetActualOrdersAsync(string userId);
		Task<List<ActualTemplatedOrderDto>> GetActualTemplatedOrdersAsync(string userId);
		Task<List<DoneOrderTitleDto>> GetDoneOrdersAsync(string userId);
		Task<OrderDto> GetOrderDetailsAsync(int orderId);
		Task<ConfirmedOrderDto> GetConfirmedOrderDetailsAsync(int orderId);
		Task<int> CreateOrderAsync(OrderCreateDto request);
		Task<int> CreateOrderTemplateAsync(OrderTemplateCreateDto request);
 		Task ConfirmOrderAsync(OrderConfirmDto request);
		Task ConfirmOrderTemplateAsync(int orderId);
		Task AddBookToOrderAsync(BookActionDto book);
		Task DeleteOrderAsync(int orderId);
		Task<bool> DeleteBookFromOrderAsync(BookActionDto book, decimal bookPrice = 0);
	}
}
