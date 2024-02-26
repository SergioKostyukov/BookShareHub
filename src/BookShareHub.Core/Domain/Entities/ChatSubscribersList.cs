namespace BookShareHub.Core.Domain.Entities;

public enum UserChatStatus
{
	Admin,
	Moderator,
	User
}

public class ChatSubscribersList
{
	public int Id { get; set; }
	public int ChatId { get; set; }
	public int UserId { get; set; }
	public UserChatStatus Status { get; set; }
	public DateTime AddedTime { get; set; }
}
