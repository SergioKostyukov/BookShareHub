using BookShareHub.Application.Dto.Order;

namespace BookShareHub.WebUI.Models
{
    public class ContractModel
	{
		public required List<ActualOrderTitleDto> OrdersTemplated { get; init; }
		public required List<ActualOrderTitleDto> OrdersByMeConfirmed { get; init; }
		public required List<ActualOrderTitleDto> OrdersToMeConfirmed { get; init; }
	}
}
