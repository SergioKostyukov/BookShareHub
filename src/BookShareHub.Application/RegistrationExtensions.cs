using BookShareHub.Application.Interfaces;
using BookShareHub.Application.MappingProfiles;
using BookShareHub.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BookShareHub.Application
{
	public static class RegistrationExtensions
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(BookProfile));
			services.AddAutoMapper(typeof(UserProfile));
			services.AddAutoMapper(typeof(OrderProfile));
			services.AddAutoMapper(typeof(RaffleProfile));

			services.AddScoped<IBooksLibraryService, BooksLibraryService>();
			services.AddScoped<IBookService, BookService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IOrderService, OrderService>();
			services.AddScoped<IRafflesLibraryService, RafflesLibraryService>();
			services.AddScoped<IRaffleService, RaffleService>();

			return services;
		}
	}
}
