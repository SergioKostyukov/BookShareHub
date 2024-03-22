using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Application.Dto.Raffle;

public record RaffleCreateDto
{
	public string OwnerId { get; set; }
	public int OrderId { get; set; }
	public RaffleType Type { get; set; }
	public decimal TicketPrice { get; set; }
	public DateTime EndDateTime { get; set; }
	public string? Description { get; set; }
}
