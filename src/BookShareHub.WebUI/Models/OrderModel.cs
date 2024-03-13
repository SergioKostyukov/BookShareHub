using BookShareHub.Application.Dto;

namespace BookShareHub.WebUI.Models;

public class OrderModel
{
	public int Id { get; set; }	
	public OrderDto? Order { get; init; } 
	public UserDto? Owner { get; set; }
	public List<BookTitleDto>? OrderList { get; set; }
}
