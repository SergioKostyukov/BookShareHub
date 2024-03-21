using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Application.Dto.Raffle;

public record RaffleTitleDto
{
	public int Id { get; init; }
	public int OrderId { get; init; }
	public RaffleType Type { get; init; }
	public decimal TicketPrice { get; init; }
	public DateTime EndDateTime { get; init; }
	public string? ImagePath { get; set; }
}

//(
//	int Id,
//	int OrderId,
//	RaffleType Type,
//	decimal TicketPrice,
//	DateTime EndDateTime
//);
