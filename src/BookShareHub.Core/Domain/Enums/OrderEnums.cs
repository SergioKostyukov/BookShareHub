namespace BookShareHub.Core.Domain.Enums
{
	public enum OrderStatus
	{
		Template,
		Request,
		Agreed,
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
