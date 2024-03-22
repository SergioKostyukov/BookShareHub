using BookShareHub.Application.Dto.Order;
using BookShareHub.Application.Dto.Raffle;

namespace BookShareHub.WebUI.Models;

public class ContractModel
{
    public required List<ActualOrderTitleDto> OrdersTemplated { get; init; }
    public required List<ActualOrderTitleDto> OrdersByMeConfirmed { get; init; }
    public required List<ActualOrderTitleDto> OrdersToMeConfirmed { get; init; }
    public required List<RaffleTitleDto> RaffleTitleDtos { get; init; }
    public required List<ActualTemplatedOrderDto> TemplateRaffleTitleDtos { get; init; }
}
