using BookShareHub.Application.Dto;
using BookShareHub.Application.Dto.Book;
using BookShareHub.Application.Dto.Order;
using BookShareHub.Application.Dto.Raffle;

namespace BookShareHub.WebUI.Models;

public class RaffleModel
{
	public required RaffleDto Raffle { get; init; }
	public required UserDto Owner { get; init; }
	public required OrderDto OrderDetails { get; set; }
	public required List<BookTitleDto> RaffleList { get; init; }
}
