using BookShareHub.Application.Dto;
using BookShareHub.Application.Dto.Book;
using BookShareHub.Application.Dto.Order;

namespace BookShareHub.WebUI.Models;

public class OrderModel
{
    public BookActionDto BookActionDetails { get; set; } = new BookActionDto();
    public decimal BookPrice { get; set; }
    public required OrderDto Order { get; set; }
    public DeliveryParams DeliveryParams { get; set; } = new DeliveryParams();
    public required UserDto Owner { get; init; }
    public required List<BookTitleDto> OrderList { get; init; }
    public required List<BookTitleDto> OtherSellerItems { get; init; }
}

public class DeliveryParams
{
	public string DeliveryAddress { get; set; } = string.Empty;
}