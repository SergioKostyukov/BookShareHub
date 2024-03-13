using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Application.Dto;

public record DoneOrderTitleDto(
	int Id,
	string CustomerId,
	string OwnerId,
	DateTime CloseDate
);

public record ActualOrderTitleDto(
	int Id,
	string CustomerId,
	string OwnerId,
	OrderType Type,
	DateTime CreateDate
);