using Bogus;
using BookShareHub.Core.Domain.Entities;
using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Infrastructure.Data.DataGeneration;

public class BookDataGeneration
{
	Faker<Book> bookDataFake;
	private const string DefaultImagePath = "/images/photo.jpg";
	private const string DefaultOwnerId = "5c99654c-cf04-497d-ab79-4411c2e7a88f";

	public BookDataGeneration()
	{
		Randomizer.Seed = new Random(20);

		bookDataFake = new Faker<Book>()
			.RuleFor(b => b.OwnerId, f => DefaultOwnerId)
			.RuleFor(b => b.Title, f => f.Commerce.ProductName())
			.RuleFor(b => b.Author, f => f.Person.FullName)
			.RuleFor(b => b.Genre, f => f.PickRandom<BookGenre>())
			.RuleFor(b => b.Language, f => f.PickRandom<BookLanguage>())
			.RuleFor(b => b.Description, f => f.Lorem.Paragraph())
			.RuleFor(b => b.Price, f => f.Random.Decimal(100, 800))
			.RuleFor(b => b.ImagePath, f => DefaultImagePath);
	}

	public IEnumerable<Book> GenerateBooks()
	{
		return bookDataFake.GenerateForever();
	}
}
