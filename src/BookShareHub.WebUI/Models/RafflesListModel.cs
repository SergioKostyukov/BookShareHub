using BookShareHub.Application.Dto.Raffle;
using BookShareHub.Application.Filters;

namespace BookShareHub.WebUI.Models;

public class RafflesListModel
{
	public required string UserId { get; set; }
	public required List<RaffleTitleDto> RaffleTitles { get; set; }
	public RaffleFilter? FilterQuery { get; set; }
	public SearchFilter? SearchQuery { get; set; }
}
