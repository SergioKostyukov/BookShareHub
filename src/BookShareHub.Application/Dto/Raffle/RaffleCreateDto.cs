using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Application.Dto.Raffle;

public record RaffleCreateDto(
	string OwnerId,
	int OrderId,
	RaffleType Type,
	decimal TicketPrice,
	DateTime EndDateTime,
	string? Description
);
