using BookShareHub.Application.Dto.Book;
using BookShareHub.Application.Dto;
using BookShareHub.Application.Dto.Order;

namespace BookShareHub.WebUI.Models;

public class HistoryModel
{
	public List<DoneOrderTitleDto> OrderTitles { get; init; }
	public OrderDto Order { get; init; }
	public UserDto User { get; init; }
	public List<BookTitleDto> OrderList { get; init; }
}
