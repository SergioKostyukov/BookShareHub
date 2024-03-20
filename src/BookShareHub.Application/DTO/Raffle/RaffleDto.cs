using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Application.Dto.Raffle;

public class RaffleDto
{
	public int Id { get; set; }
	public string OwnerId { get; set; } = string.Empty;
	public DateTime EndDateTime { get; set; }
	public string Title { get; set; } = string.Empty;
	public string Author { get; set; } = string.Empty;
	public BookGenre Genre { get; set; }
	public BookLanguage Language { get; set; }
	public string? Description { get; set; }
	public decimal TicketPrice { get; set; }
	public string ImagePath { get; set; } = string.Empty;
}
