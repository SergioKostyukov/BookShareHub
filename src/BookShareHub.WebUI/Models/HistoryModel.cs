using BookShareHub.Application.Dto;

namespace BookShareHub.WebUI.Models;

public class HistoryModel
{
	public List<DoneOrderDetailsDto> OrderTitles { get; set; }
	//public OrderDto? OrderDetails { get; set; }
	//public List<BookDto>? OrderBookList { get; set; }
}
