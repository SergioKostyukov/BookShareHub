using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Application.Filters
{
	public class RafflesLibraryFilter
	{
		public RaffleType? SelectedType { get; set; }
		public decimal? SelectedMaxTicketPrice { get; set; }
	}
}
