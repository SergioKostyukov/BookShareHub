using BookShareHub.Application.Interfaces;
using BookShareHub.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BookShareHub.Application
{
	public static class RegistrationExtensions
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			services.AddScoped<IBookService, BookService>();

			return services;
		}
	}
}
