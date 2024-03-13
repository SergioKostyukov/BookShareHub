using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Application.Dto;

public record OrderDto(
	int Id,
	string CustomerId,
	string OwnerId,
	OrderStatus Status,
	OrderType Type,
	DateTime CreateDate,
	DateTime CloseDate,
	decimal CheckAmount
);

public record OrderCreateDto (
	string CustomerId,
	string OwnerId,
	int BookId,
	OrderType? Type,
	decimal CheckAmount
);

public record DoneOrderDetailsDto(
	int Id,
	string CustomerId,
	string OwnerId,
	DateTime CloseDate
);