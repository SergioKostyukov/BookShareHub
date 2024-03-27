using BookShareHub.Application.Dto;
using BookShareHub.Application.Dto.Book;
using BookShareHub.Application.Dto.Order;

namespace BookShareHub.WebUI.Models;

public class ConfirmedOrderModel
{
    public required ConfirmedOrderDto Order { get; init; }
    public required UserDto User { get; init; }
    public required List<BookTitleDto> OrderList { get; init; }
    public required string UserId { get; init; }
}