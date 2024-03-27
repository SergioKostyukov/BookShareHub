namespace BookShareHub.Application.Dto.Raffle;

public record AddRaffleParticipantDto(
	int RaffleId,
	string UserId,
	int TicketsCount,
	string DeliveryAddress,
	string DeliveryUser,
	string DeliveryUserPhone
);

