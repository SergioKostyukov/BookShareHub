using BookShareHub.Application.Dto;

namespace BookShareHub.WebUI.Models
{
	public class ContractModel
	{
		public required List<ActualOrderTitleDto> OrderTitles { get; init; }
	}
}
