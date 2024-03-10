using BookShareHub.Application.Dto;
using BookShareHub.Core.Domain.Entities;

namespace BookShareHub.WebUI.Models;

public class OrderModel
{
	public BookDto Book { get; set; } 
	public OrderDto Order { get; set; } 
	public UserDto Owner { get; set; }
	public List<OrderList>? OrderList { get; set; }
}
