namespace BookShareHub.Core.Domain.Entities;

public class RaffleParticipantsList
{
	public int RaffleId { get; set; }
	public string UserId { get; set; } = string.Empty;
	public int TicketsCount { get; set; }
	public DateTime ParticipationTime { get; set; }
	public string DeliveryAddress { get; set; } = string.Empty;
	public string DeliveryUser { get; set; } = string.Empty;
	public string DeliveryUserPhone { get; set; } = string.Empty;
}
