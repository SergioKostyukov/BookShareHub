using Microsoft.AspNetCore.Identity;

namespace BookShareHub.Core.Domain.Entities;

public class User : IdentityUser
{
	public float? Rating { get; set; }
}
