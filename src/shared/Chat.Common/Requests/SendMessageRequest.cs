namespace Chat.Common.Requests;

public class SendMessageRequest
{
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public int RoomId { get; set; }
    public int UserId { get; set; }
}