using BookShareHub.Application.Dto;

namespace BookShareHub.WebUI.Models;

public class HistoryModel
{
	public required List<DoneOrderTitleDto> OrderTitles { get; init; }
}
