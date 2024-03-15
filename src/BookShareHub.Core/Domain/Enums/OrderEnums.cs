namespace BookShareHub.Core.Domain.Enums
{
	public enum OrderStatus
	{
		Template,
		Confirmed,
		Sended,
		Done,
		Canceled
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
