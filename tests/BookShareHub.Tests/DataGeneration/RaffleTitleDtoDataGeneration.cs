using Bogus;
using BookShareHub.Application.Dto.Book;
using BookShareHub.Application.Dto.Raffle;
using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Tests.DataGeneration
{
	public class RaffleTitleDtoDataGeneration
	{
		readonly Faker<RaffleTitleDto> bookDataFake;

		public RaffleTitleDtoDataGeneration()
		{
			bookDataFake = new Faker<RaffleTitleDto>()
					.RuleFor(b => b.Id, f => f.Random.Int(1, 100))
					.RuleFor(b => b.OrderId, f => f.Random.Int(1, 100))
					.RuleFor(b => b.Type, f => f.PickRandom<RaffleType>())
					.RuleFor(b => b.TicketPrice, f => f.Random.Decimal(100, 800))
					.RuleFor(b => b.EndDateTime, f => f.Date.Recent(3))
					.RuleFor(b => b.ImagePath, f => f.Random.String());
		}

		public RaffleTitleDto GenerateBook()
		{
			return bookDataFake.Generate();
		}

		public List<RaffleTitleDto> GenerateBooks(int count)
		{
			return bookDataFake.Generate(count);
		}
	}
}
