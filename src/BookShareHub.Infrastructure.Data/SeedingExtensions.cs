using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace BookShareHub.Infrastructure.Data
{
	public static class SeedingExtensions
	{
		public static async Task DarabaseEnsureCreated(this IApplicationBuilder applicationBuilder)
		{
			using(var scope = applicationBuilder.ApplicationServices.CreateScope())
			{
				var dbContext = scope.ServiceProvider.GetRequiredService<BookShareHubDbContext>();
				var database = dbContext.Database;

				await database.EnsureDeletedAsync();
				await database.EnsureCreatedAsync();
			}
		}

		// initialize default data in tables
	}
}
