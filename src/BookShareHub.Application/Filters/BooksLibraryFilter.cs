using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Application.Filters
{
	public class BooksLibraryFilter
	{
		public BookLanguage? SelectedLanguage { get; set; }
		public BookGenre? SelectedGenre { get; set; }
		public decimal? MaxPrice { get; set; }
	}
}
