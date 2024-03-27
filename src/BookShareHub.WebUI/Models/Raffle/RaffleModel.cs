using BookShareHub.Application.Dto;
using BookShareHub.Application.Dto.Book;
using BookShareHub.Application.Dto.Order;
using BookShareHub.Application.Dto.Raffle;

namespace BookShareHub.WebUI.Models;

public class RaffleModel
{
	public RaffleDto Raffle { get; init; }
	public required UserDto Owner { get; init; }
	public required OrderDto OrderDetails { get; set; }
	public int TicketsCount { get; set; } = 0;
	public required List<BookTitleDto> RaffleList { get; init; }
	public required DeliveryParams DeliveryParams { get; set; } = new DeliveryParams();
}
