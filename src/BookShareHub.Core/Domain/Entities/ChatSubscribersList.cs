namespace BookShareHub.Core.Domain.Entities;

public class ChatSubscribersList
{
	public int Id { get; set; }
	public int ChatId { get; set; }
	public int UserId { get; set; }
	public int Status { get; set; } // admin/moderator/user
	public DateTime AddedTime { get; set; }
}
