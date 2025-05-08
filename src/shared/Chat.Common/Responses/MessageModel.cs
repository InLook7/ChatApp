namespace Chat.Common.Responses;

public class MessageModel
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public int RoomId { get; set; }
    public int? UserId { get; set; }
    public string? UserName { get; set; }
}