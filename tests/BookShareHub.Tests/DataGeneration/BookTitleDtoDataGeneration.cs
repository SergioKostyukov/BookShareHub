using Bogus;
using BookShareHub.Application.Dto.Book;
using BookShareHub.Core.Domain.Entities;
using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Tests.DataGeneration
{
	public class BookTitleDtoDataGeneration
	{
		readonly Faker<BookTitleDto> bookDataFake;

		public BookTitleDtoDataGeneration()
		{
			bookDataFake = new Faker<BookTitleDto>()
			.CustomInstantiator(f => new BookTitleDto(
				f.Random.Int(1, 100),
				f.Commerce.ProductName(),
				f.Person.FullName,
				f.PickRandom<BookGenre>(),
				f.Random.String(),
				f.Random.Decimal(100, 800)
			));
		}

		public BookTitleDto GenerateBook()
		{
			return bookDataFake.Generate();
		}

		public List<BookTitleDto> GenerateBooks(int count)
		{
			return bookDataFake.Generate(count);
		}
	}
}
