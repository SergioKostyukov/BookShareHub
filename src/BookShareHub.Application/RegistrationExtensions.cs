 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using BookShareHub.Application.Interfaces;
using BookShareHub.Application.Services;

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
