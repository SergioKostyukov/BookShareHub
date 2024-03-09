using BookShareHub.Application.Dto;

namespace BookShareHub.WebUI.Models;

public class OrderModel
{
	public BookDto Book { get; set; } = new BookDto();
	public OrderDto Order { get; set; } = new OrderDto();
	
}
