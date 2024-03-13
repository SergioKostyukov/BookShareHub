using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Application.Dto;

public record BookTitleDto (
	int Id, 
	string Title, 
	string Author, 
	BookGenre Genre, 
	string ImagePath,
	decimal? Price
);
