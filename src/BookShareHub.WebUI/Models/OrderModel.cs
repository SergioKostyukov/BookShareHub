using BookShareHub.Application.Dto;

namespace BookShareHub.WebUI.Models;

public class OrderModel
{
	public int? SelectedBookId { get; set; }
	public required OrderDto Order { get; set; } 
	public required UserDto Owner { get; init; }
	public required List<BookTitleDto> OrderList { get; init; }
	public required List<BookTitleDto> OtherSellerItems { get; init; }
}
