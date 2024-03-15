using BookShareHub.Application.Dto.Order;

namespace BookShareHub.WebUI.Models;

public class HistoryModel
{
	public required List<DoneOrderTitleDto> OrderTitles { get; init; }
}
