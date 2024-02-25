namespace BookShareHub.Core.Domain.Entities;

public class User
{
	public int Id {  get; set; }
	public string Name { get; set; }
	public string Tag { get; set; }
	public string? Email { get; set; }
	public string Password { get; set; }
	public float? Rating { get; set; }
}
