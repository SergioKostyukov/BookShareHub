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

				await dbContext.Database.MigrateAsync();

				await SeedInitialUsersData(dbContext);
				await SeedInitialBooksData(dbContext);
			}
		}

		// initialize default data in tables
		private static async Task SeedInitialUsersData(BookShareHubDbContext dbContext)
		{
			var userDataGeneration = new UserDataGeneration();

			//var users = userDataGeneration.GenerateUsers().Take(10);
			//dbContext.Users.AddRange(users);
			var user = userDataGeneration.GenerateUser();
			dbContext.Users.Add(user);

			await dbContext.SaveChangesAsync();
		}

		private static async Task SeedInitialBooksData(BookShareHubDbContext dbContext)
		{
			var bookDataGeneration = new BookDataGeneration();

			var firstUserId = await dbContext.Users.Select(u => u.Id).FirstOrDefaultAsync();
			var books = bookDataGeneration.GenerateBooks().Take(10);
			foreach (var book in books)
			{
				book.OwnerId = firstUserId.ToString();
			}
			dbContext.Books.AddRange(books);

			await dbContext.SaveChangesAsync();
		}
	}
}
