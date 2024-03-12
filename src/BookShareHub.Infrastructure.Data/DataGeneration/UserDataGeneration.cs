using Bogus;
using BookShareHub.Core.Domain.Entities;

namespace BookShareHub.Infrastructure.Data.DataGeneration;

public class UserDataGeneration
{
	readonly Faker<User> userDataFake;

	public UserDataGeneration()
	{
		Randomizer.Seed = new Random(20);

		userDataFake = new Faker<User>()
				.RuleFor(u => u.Rating, f => f.Random.Float(1, 5))
				.RuleFor(u => u.UserName, f => f.Name.FullName())
				.RuleFor(u => u.NormalizedUserName, (f, u) => u.UserName.ToUpper())
				.RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.UserName))
				.RuleFor(u => u.EmailConfirmed, f => f.Random.Bool())
				.RuleFor(u => u.PasswordHash, f => f.Internet.Password())
				.RuleFor(u => u.SecurityStamp, f => Guid.NewGuid().ToString())
				.RuleFor(u => u.ConcurrencyStamp, f => Guid.NewGuid().ToString())
				.RuleFor(u => u.PhoneNumberConfirmed, f => false)
				.RuleFor(u => u.TwoFactorEnabled, f => false)
				.RuleFor(u => u.LockoutEnabled, f => true)
				.RuleFor(u => u.AccessFailedCount, f => 0);
	}

	public User GenerateUser()
	{
		return userDataFake.Generate();
	}

	public IEnumerable<User> GenerateUsers()
	{
		return userDataFake.GenerateForever();
	}
}
