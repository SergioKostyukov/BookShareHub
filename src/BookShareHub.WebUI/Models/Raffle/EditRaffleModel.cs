using BookShareHub.Application.Dto.Raffle;

namespace BookShareHub.WebUI.Models.Raffle;

public class EditRaffleModel
{
	public required RaffleDto Raffle { get; init; }

	public IFormFile? ImageFile { get; set; }
}
