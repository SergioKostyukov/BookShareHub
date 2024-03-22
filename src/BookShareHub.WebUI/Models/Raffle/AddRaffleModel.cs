using BookShareHub.Application.Dto.Book;
using BookShareHub.Application.Dto.Raffle;
using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.WebUI.Models;

public class AddRaffleModel
{
	public string OwnerId { get; set; } = string.Empty;
	public int OrderId { get; set; }
	public RaffleCreateDto RaffleCreate { get; set; } = new RaffleCreateDto();
	public BookActionDto BookActionDetails { get; set; } = new BookActionDto();
	public List<BookTitleDto>? RaffleList { get; init; }
	public List<BookTitleDto>? MyOtherBooks { get; init; }
}
