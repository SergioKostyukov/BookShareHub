using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShareHub.Core.Domain.Enums
{
	public enum OrderStatus
	{
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
