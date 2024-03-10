using AutoMapper;
using BookShareHub.Application.Dto;
using BookShareHub.Core.Domain.Entities;

namespace BookShareHub.Application.MappingProfiles
{
	internal class BookProfile : Profile
	{
        public BookProfile()
        {
            CreateMap<Book, BookDto>();
			CreateMap<Book, BookTitleDto>();
			CreateMap<BookDto, Book>();
		}
	}
}
