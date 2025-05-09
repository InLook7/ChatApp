namespace Chat.Domain.Entities;

public class Sentiment
{
    public int Id { get; set; }
    public int MessageId { get; set; }
    public string SentimentResult { get; set; }
    public double PositiveScore { get; set; }
    public double NeutralScore { get; set; }
    public double NegativeScore { get; set; }

    // Navigation properties
    public Message Message { get; set; }
}