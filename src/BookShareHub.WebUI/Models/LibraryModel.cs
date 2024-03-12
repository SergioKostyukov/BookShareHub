using BookShareHub.Application.Dto;
using BookShareHub.Application.Filters;

namespace BookShareHub.WebUI.Models;

public class LibraryModel
{
	public List<BookTitleDto> BookTitles { get; set; }
	public LibraryFilter? FilterQuery { get; set; }
	public LibrarySearch? SearchQuery { get; set; }
}
