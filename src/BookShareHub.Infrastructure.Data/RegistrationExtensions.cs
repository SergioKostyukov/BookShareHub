using BookShareHub.Infrastructure.Data.DataGeneration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookShareHub.Infrastructure.Data
{
	public static class RegistrationExtensions
	{
		public static void AddStorage(this IServiceCollection serviceCollection, IConfiguration configuration)
		{
			serviceCollection.AddDbContext<BookShareHubDbContext>(options =>
			{
				options.UseSqlServer(configuration["ConnectionStrings:LocalDbSqlServer"], 
					options => options.MigrationsAssembly(typeof(BookShareHubDbContext).Assembly.FullName));
			});
		}

		public static IServiceCollection AddBogusServices(this IServiceCollection services)
		{
			services.AddTransient<UserDataGeneration>();
			services.AddTransient<BookDataGeneration>();
			services.AddTransient<RaffleDataGeneration>();

			return services;
		}
	}
}
