using BookShareHub.Infrastructure.Data.DataGeneration;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace BookShareHub.Infrastructure.Data
{
	public static class SeedingExtensions
	{
		public static async Task DatabaseEnsureCreated(this IApplicationBuilder applicationBuilder)
		{
			using var scope = applicationBuilder.ApplicationServices.CreateScope();
			var dbContext = scope.ServiceProvider.GetRequiredService<BookShareHubDbContext>();

			//await ClearExistingTables(dbContext);

			await dbContext.Database.MigrateAsync();

			if (!await dbContext.AspNetUsers.AnyAsync())
			{
				await SeedInitialUsersData(dbContext);
			}

			if (!await dbContext.Books.AnyAsync())
			{
				await SeedInitialBooksData(dbContext);
			}

			if(!await dbContext.Raffles.AnyAsync())
			{
				await SeedInitialRafflesData(dbContext);
			}
		}

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
		
		private static async Task SeedInitialRafflesData(BookShareHubDbContext dbContext)
		{
			var raffleDataGeneration = new RaffleDataGeneration();

			var users = await dbContext.Users.Take(10).ToListAsync();

			foreach (var user in users)
			{
				var raffles = raffleDataGeneration.GenerateRaffles(2, user.Id);
				dbContext.Raffles.AddRange(raffles);
			}

			await dbContext.SaveChangesAsync();
		}

		private static async Task ClearExistingTables(BookShareHubDbContext dbContext)
		{
			// Rebuild the database
			// var database = dbContext.Database;
			// await database.EnsureDeletedAsync();
			// await database.EnsureCreatedAsync();

			// Remove data from the table users and books to refill them
			//dbContext.AspNetUsers.RemoveRange(dbContext.AspNetUsers);
			//dbContext.Books.RemoveRange(dbContext.Books);
			dbContext.Orders.RemoveRange(dbContext.Orders);
			dbContext.OrdersLists.RemoveRange(dbContext.OrdersLists);
			dbContext.Raffles.RemoveRange(dbContext.Raffles);

			// Reset books availability
			var books = await dbContext.Books
				.Where(x => x.IsActive == false)
				.ToListAsync();

			foreach (var book in books)
			{
				book.IsActive = true;
			}
			dbContext.Books.UpdateRange(books);

			await dbContext.SaveChangesAsync();
		}
	}
}
