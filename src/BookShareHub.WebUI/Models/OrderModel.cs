using BookShareHub.Application.Dto;

namespace BookShareHub.WebUI.Models;

public class OrderModel
{
	public required int Id { get; init; }	
	public OrderDto? Order { get; set; } 
	public UserDto? Owner { get; set; }
	public List<BookTitleDto>? OrderList { get; set; }
}
