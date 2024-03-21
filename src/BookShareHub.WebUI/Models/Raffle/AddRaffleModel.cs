using BookShareHub.Application.Dto.Book;
using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.WebUI.Models;

public class AddRaffleModel
{
	public string OwnerId { get; set; }
	public RaffleType Type { get; set; }
	public decimal TicketPrice { get; set; }
	public DateTime EndDateTime { get; set; }
	public string? Description { get; set; }
	public int OrderId { get; set; }
	public BookDeleteDto DeleteBookDetails { get; set; } = new BookDeleteDto();
	public List<BookTitleDto>? RaffleList { get; init; }
	public List<BookTitleDto>? MyOtherBooks { get; init; }
}
