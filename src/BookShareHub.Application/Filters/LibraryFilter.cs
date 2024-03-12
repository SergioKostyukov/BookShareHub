using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Application.Filters
{
	public class LibraryFilter
	{
		public BookLanguage? SelectedLanguage { get; set; }
		public BookGenre? SelectedGenre { get; set; }
		public decimal? MaxPrice { get; set; }
	}

	public class LibrarySearch
	{
        public string? Request { get; set; }
    }
}
