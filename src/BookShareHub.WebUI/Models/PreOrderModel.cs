using BookShareHub.Application.Dto;
using BookShareHub.Application.Dto.Book;

namespace BookShareHub.WebUI.Models;

public class PreOrderModel
{
	public required BookDto Book { get; init; }
	public required UserDto Owner { get; init; }
}
