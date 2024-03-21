using BookShareHub.Application.Dto.Book;
using BookShareHub.Application.Dto.Order;

namespace BookShareHub.Application.Interfaces
{
    public interface IOrderService
	{
		Task<List<ActualOrderTitleDto>> GetActualOrdersAsync(string userId);
		Task<List<DoneOrderTitleDto>> GetDoneOrdersAsync(string userId);
		Task<OrderDto> GetOrderDetailsAsync(int orderId);
		Task<int> CreateOrderAsync(OrderCreateDto request);
		Task<int> CreateOrderTemplateAsync(OrderTemplateCreateDto request);
 		Task ConfirmOrderAsync(OrderConfirmDto request);
		Task ConfirmOrderTemplateAsync(int orderId);
		Task AddBookToOrder(int orderId, int bookId);
		Task DeleteOrderAsync(int orderId);
		Task<bool> DeleteBookFromOrderAsync(BookDeleteDto book);
		Task<bool> DeleteBookFromRaffleOrderAsync(BookDeleteDto book);
	}
}
