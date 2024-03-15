﻿using BookShareHub.Application.Dto;
using BookShareHub.Application.Dto.Book;
using BookShareHub.Application.Dto.Order;

namespace BookShareHub.WebUI.Models;

public class OrderModel
{
	public BookDeleteDto DeleteBookDetails { get; set; } = new BookDeleteDto();
	public required OrderDto Order { get; set; } 
	public required UserDto Owner { get; init; }
	public required List<BookTitleDto> OrderList { get; init; }
	public required List<BookTitleDto> OtherSellerItems { get; init; }
}
