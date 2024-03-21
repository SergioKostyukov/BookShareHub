namespace BookShareHub.Core.Domain.Enums
{
	public enum OrderStatus
	{
		Template,
		Confirmed,
		Sended,
		Done,
		Canceled,
		Active
	}

	public enum OrderType
	{
		Free,
		Trade,
		Sale,
		Raffle,
		Auction
	}
}
