using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Core.Domain.Entities;

public class Raffle
{
	public int Id { get; set; }
	public string OwnerId { get; set; }
	public int OrderId { get; set; }
	public RaffleType Type { get; set; }
	public decimal TicketPrice { get; set; }
	public DateTime EndDateTime { get; set; }
	public string? Description { get; set; }
	public bool IsActive { get; set; } = true;
}
