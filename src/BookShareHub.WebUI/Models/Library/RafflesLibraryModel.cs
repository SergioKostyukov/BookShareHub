using BookShareHub.Application.Dto.Raffle;
using BookShareHub.Application.Filters;

namespace BookShareHub.WebUI.Models;

public class RafflesLibraryModel
{
    public required string UserId { get; set; }
    public required List<RaffleTitleDto> RaffleTitles { get; set; }
    public RafflesLibraryFilter? FilterQuery { get; set; }
}
