namespace BookShareHub.Core.Domain.Entities;

public enum ChatType
{
	Public,
	Private
}

public class Chat
{
	public int Id { get; set; }
	public int AdminId { get; set; } // possible value duplication in "ChatSubscribersList" 
	public string Title { get; set; }
	public string? Description { get; set; }
	public DateTime CreateDate { get; set; }
	public ChatType ChatType { get; set; }
	public string Messages { get; set; }
}
