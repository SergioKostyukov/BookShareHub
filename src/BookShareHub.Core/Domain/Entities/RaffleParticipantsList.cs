namespace BookShareHub.Core.Domain.Entities;

public class RaffleParticipantsList
{
	public int RaffleId { get; set; }
	public string RaffleUserId { get; set; } = string.Empty;
	public int TicketsCount { get; set; }
	public DateTime ParticipationTime { get; set; }
	// delivery parameters
}
