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

				// Rebuild the database
				//var database = dbContext.Database;
				//await database.EnsureDeletedAsync();
				//await database.EnsureCreatedAsync();

				// Remove data from the table users and books to refill them
				//dbContext.AspNetUsers.RemoveRange(dbContext.AspNetUsers);
				//dbContext.Books.RemoveRange(dbContext.Books);
				//await dbContext.SaveChangesAsync();

				if (!await dbContext.AspNetUsers.AnyAsync() && !await dbContext.Books.AnyAsync())
				{
					await dbContext.Database.MigrateAsync();

					await SeedInitialUsersData(dbContext);
					await SeedInitialBooksData(dbContext);
				}
			}
		}

		// Fill the user table with initial data
		private static async Task SeedInitialUsersData(BookShareHubDbContext dbContext)
		{
			var userDataGeneration = new UserDataGeneration();

			var users = userDataGeneration.GenerateUsers().Take(10);

			foreach (var user in users)
			{
				dbContext.AspNetUsers.Add(user);
			}

			await dbContext.SaveChangesAsync();
		}

		// Fill the books table with initial data
		private static async Task SeedInitialBooksData(BookShareHubDbContext dbContext)
		{
			var bookDataGeneration = new BookDataGeneration();

			var users = await dbContext.Users.Take(10).ToListAsync();

			foreach (var user in users)
			{
				var books = bookDataGeneration.GenerateBooks(5, user.Id);
				dbContext.Books.AddRange(books);
			}

			await dbContext.SaveChangesAsync();
		}
	}
}
