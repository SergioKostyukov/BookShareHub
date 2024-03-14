using Bogus;
using BookShareHub.Core.Domain.Entities;
using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Infrastructure.Data.DataGeneration;

public class BookDataGeneration
{
	readonly Faker<Book> bookDataFake;
	private const string DefaultImagePath = "/images/photo.jpg";

	public BookDataGeneration()
	{
		Randomizer.Seed = new Random(20);

		bookDataFake = new Faker<Book>()
			.RuleFor(b => b.Title, f => f.Commerce.ProductName())
			.RuleFor(b => b.Author, f => f.Person.FullName)
			.RuleFor(b => b.Genre, f => f.PickRandom<BookGenre>())
			.RuleFor(b => b.Language, f => f.PickRandom<BookLanguage>())
			.RuleFor(b => b.Description, f => f.Lorem.Paragraph())
			.RuleFor(b => b.Price, f => f.Random.Decimal(100, 800))
			.RuleFor(b => b.ImagePath, f => DefaultImagePath)
			.RuleFor(b => b.IsActive, true);
	}

	public IEnumerable<Book> GenerateBooks(int count, string ownerId)
	{
		return bookDataFake
			.RuleFor(b => b.OwnerId, f => ownerId)
			.Generate(count);
	}
}
