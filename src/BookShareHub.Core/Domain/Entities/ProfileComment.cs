namespace BookShareHub.Core.Domain.Entities;

public class ProfileComment
{
	public int Id { get; set; }
	public int AuthorId { get; set; }
	public int ProfileId { get; set; }
	public string Comment { get; set; } = string.Empty;
	public int Mark { get; set; }
	public string? Response { get; set; }
	public DateTime CreateDate { get; set; }
}
