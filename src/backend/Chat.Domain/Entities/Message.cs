namespace Chat.Domain.Entities;

public class Message
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public int RoomId { get; set; }
    public int? UserId { get; set; }

    // Navigation properties
    public Room Room { get; set; }
    public User User { get; set; }
}