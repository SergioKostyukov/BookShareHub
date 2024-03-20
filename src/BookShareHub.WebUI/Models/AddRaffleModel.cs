using BookShareHub.Application.Dto.Raffle;

namespace BookShareHub.WebUI.Models
{
	public class AddRaffleModel
	{
		public required RaffleDto Raffle { get; init; }
		public required IFormFile ImageFile { get; set; }
	}
}
