using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Core.Domain.Entities;

public class Chat
{
	public int Id { get; set; }
	public int AdminId { get; set; } // possible value duplication in "ChatSubscribersList" 
	public string Title { get; set; } = string.Empty;
	public string? Description { get; set; }
	public DateTime CreateDate { get; set; }
	public ChatType ChatType { get; set; }
	public string Messages { get; set; } = string.Empty;
}
