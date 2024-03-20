using Bogus;
using BookShareHub.Core.Domain.Entities;
using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Infrastructure.Data.DataGeneration;

public class RaffleDataGeneration
{
	readonly Faker<Raffle> raffleDataFake;
	private const string DefaultImagePath = "https://storage.googleapis.com/book_share_hub_books_images/photo.jpg";

	public RaffleDataGeneration()
	{
		Randomizer.Seed = new Random(20);

		raffleDataFake = new Faker<Raffle>()
			.RuleFor(b => b.Type, f => f.PickRandom<RaffleType>())
			.RuleFor(b => b.Description, f => f.Lorem.Paragraph())
			.RuleFor(b => b.TicketPrice, f => f.Random.Decimal(30, 100))
			.RuleFor(b => b.EndDateTime, f => f.Date.Recent(3))
			.RuleFor(b => b.Title, f => f.Commerce.ProductName())
			.RuleFor(b => b.Author, f => f.Person.FullName)
			.RuleFor(b => b.Genre, f => f.PickRandom<BookGenre>())
			.RuleFor(b => b.Language, f => f.PickRandom<BookLanguage>())
			.RuleFor(b => b.ImagePath, f => DefaultImagePath)
			.RuleFor(b => b.IsActive, true);
	}

	public IEnumerable<Raffle> GenerateRaffles(int count, string ownerId)
	{
		return raffleDataFake
			.RuleFor(b => b.OwnerId, f => ownerId)
			.Generate(count);
	}
}
