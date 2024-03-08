using Microsoft.AspNetCore.Identity;

namespace BookShareHub.Core.Domain.Entities;

public class User : IdentityUser
{
	public decimal Balance { get; set; } = 0;
	public float? Rating { get; set; }
}
