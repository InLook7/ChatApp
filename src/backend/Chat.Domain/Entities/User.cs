namespace Chat.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }

    // Navigation properties
    public ICollection<Message> Messages { get; set; }
}