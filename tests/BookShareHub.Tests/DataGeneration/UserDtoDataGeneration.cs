using Bogus;
using BookShareHub.Application.Dto;
using BookShareHub.Application.Dto.Book;
using BookShareHub.Core.Domain.Entities;
using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Tests.DataGeneration
{
	public class UserDtoDataGeneration
	{
		readonly Faker<UserDto> userDataFake;

		public UserDtoDataGeneration()
		{
			userDataFake = new Faker<UserDto>()
			.CustomInstantiator(f => new UserDto(
				f.Random.String(),
				f.Person.FullName,
				f.Random.Float(100, 800)
			));
		}

		public UserDto GenerateUser()
		{
			return userDataFake.Generate();
		}

		public List<UserDto> GenerateUsers(int count)
		{
			return userDataFake.Generate(count);
		}
	}
}
