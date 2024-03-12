using BookShareHub.Infrastructure.Data.DataGeneration;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookShareHub.Infrastructure.Data
{
	public static class SeedingExtensions
	{
		public static async Task DatabaseEnsureCreated(this IApplicationBuilder applicationBuilder)
		{
			using (var scope = applicationBuilder.ApplicationServices.CreateScope())
			{
				var dbContext = scope.ServiceProvider.GetRequiredService<BookShareHubDbContext>();
				var database = dbContext.Database;

				//await database.EnsureDeletedAsync();
				//await database.EnsureCreatedAsync();

				dbContext.Books.RemoveRange(dbContext.Books);
				await dbContext.SaveChangesAsync();

				await dbContext.Database.MigrateAsync();

				await SeedInitialUsersData(dbContext);
				await SeedInitialBooksData(dbContext);
			}
		}

		// initialize default data in tables
		private static async Task SeedInitialUsersData(BookShareHubDbContext dbContext)
		{
			var userDataGeneration = new UserDataGeneration();

			var user = userDataGeneration.GenerateUser();
			if (!(await dbContext.AspNetUsers.AnyAsync(u => u.UserName == user.UserName)))
			{
				dbContext.AspNetUsers.Add(user);

				await dbContext.SaveChangesAsync();
			}
		}

		private static async Task SeedInitialBooksData(BookShareHubDbContext dbContext)
		{
			var bookDataGeneration = new BookDataGeneration();

			var firstUserId = await dbContext.Users.Select(u => u.Id).FirstOrDefaultAsync();

			var books = bookDataGeneration.GenerateBooks(10, firstUserId.ToString());

			dbContext.Books.AddRange(books);

			await dbContext.SaveChangesAsync();
		}
	}
}
