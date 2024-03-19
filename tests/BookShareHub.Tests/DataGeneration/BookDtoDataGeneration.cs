using Bogus;
using BookShareHub.Application.Dto.Book;
using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Tests.DataGeneration
{
	public class BookDtoDataGeneration
	{
		readonly Faker<BookDto> bookDataFake;

		public BookDtoDataGeneration()
		{
			bookDataFake = new Faker<BookDto>()
					.RuleFor(b => b.Id, f => f.Random.Int(1, 100))
					.RuleFor(b => b.OwnerId, f => Guid.NewGuid().ToString())
					.RuleFor(b => b.Title, f => f.Commerce.ProductName())
					.RuleFor(b => b.Author, f => f.Person.FullName)
					.RuleFor(b => b.Genre, f => f.PickRandom<BookGenre>())
					.RuleFor(b => b.Language, f => f.PickRandom<BookLanguage>())
					.RuleFor(b => b.Description, f => f.Lorem.Paragraph())
					.RuleFor(b => b.Price, f => f.Random.Decimal(100, 800))
					.RuleFor(b => b.ImagePath, f => f.Random.String());
		}

		public BookDto GenerateBook()
		{
			return bookDataFake.Generate();
		}

		public IEnumerable<BookDto> GenerateBooks()
		{
			return bookDataFake.GenerateForever();
		}
	}
}
