using BookShareHub.Application.Dto;

namespace BookShareHub.WebUI.Models;

public class PreOrderModel
{
	public required BookDto Book { get; init; }
	public required UserDto Owner { get; init; }
}
