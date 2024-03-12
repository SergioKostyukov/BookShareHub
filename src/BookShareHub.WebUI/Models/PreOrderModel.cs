using BookShareHub.Application.Dto;

namespace BookShareHub.WebUI.Models;

public class PreOrderModel
{
	public BookDto Book { get; set; }
	public UserDto Owner { get; set; }
}
