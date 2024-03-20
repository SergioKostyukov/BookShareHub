using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Application.Dto.Raffle
{
	public record RaffleTitleDto(
		int Id,
		RaffleType Type,
		string Title,
		string ImagePath,
		DateTime EndDateTime
	);
}
