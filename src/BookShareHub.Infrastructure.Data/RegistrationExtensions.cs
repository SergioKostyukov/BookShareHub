using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace BookShareHub.Infrastructure.Data
{
	public static class RegistrationExtensions
	{
		public static void AddStorage(this IServiceCollection serviceCollection, IConfiguration configuration)
		{
			serviceCollection.AddDbContext<BookShareHubDbContext>(options =>
			{
				options.UseSqlServer(configuration["ConnectionStrings:LocalDbSqlServer"], options => options.MigrationsAssembly("BookShareHub.Infrastructure.Data"));
			});
		}
	}
}
