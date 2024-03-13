using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Application.Dto;

public record OrderCreateDto(
	string CustomerId,
	string OwnerId,
	int BookId,
	OrderType? Type,
	decimal CheckAmount
);
