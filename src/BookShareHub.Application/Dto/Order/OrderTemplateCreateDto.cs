using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Application.Dto.Order
{
	public class OrderTemplateCreateDto
	{
		public string OwnerId { get; set; } = string.Empty;
		public OrderType? Type { get; set; }
	};
}
