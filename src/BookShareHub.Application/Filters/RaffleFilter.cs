﻿using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Application.Filters
{
	public class RaffleFilter
	{
		public BookLanguage? SelectedLanguage { get; set; }
		public RaffleType? SelectedType { get; set; }
		public decimal? SelectedMaxTicketPrice { get; set; }
	}
}
