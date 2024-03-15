using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Application.Dto.Order;

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
