using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Application.Dto.Order;

public record OrderCreateDto(
    string CustomerId,
    string OwnerId,
    int BookId,
    OrderType? Type,
    decimal CheckAmount
);
