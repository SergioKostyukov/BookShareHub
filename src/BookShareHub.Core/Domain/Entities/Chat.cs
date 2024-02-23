namespace BookShareHub.Core.Domain.Entities;

internal class Chat
{
	public int Id { get; set; }
	public int AdminID { get; set; } // possible value duplication in "ChatSubscribersList" 
	public string Title { get; set; }
	public string? Description { get; set; }
	public DateTime CreateDate { get; set; }
	public int Type { get; set; } // public/private
	public string Messages { get; set; }
}
