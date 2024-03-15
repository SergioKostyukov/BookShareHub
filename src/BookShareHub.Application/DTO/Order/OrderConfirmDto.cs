using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Application.Dto.Order;

public record OrderConfirmDto(
	int OrderId,
	string OwnerId,
	string OwnerName
);
