using System.Globalization;
using AutoMapper;
using BookShareHub.Application.Dto.Book;
using BookShareHub.Core.Domain.Entities;

namespace BookShareHub.Application.MappingProfiles
{
    internal class BookProfile : Profile
	{
		public BookProfile()
		{
			CreateMap<Book, BookDto>();
			CreateMap<Book, BookTitleDto>();
				//.ForMember(dest => dest.Price,
				//		   src => src.MapFrom(x => x.Price.ToString("C", CultureInfo.GetCultureInfo("uk-UA")))); 
			CreateMap<BookDto, Book>();
		}
	}
}
