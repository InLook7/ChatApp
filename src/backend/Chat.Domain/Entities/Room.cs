namespace Chat.Domain.Entities;

public class Room
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Navigation properties
    public ICollection<Message> Messages { get; set; }
}