using Chat.Application.Dtos;

namespace Chat.Application.Interfaces;

public interface ISentimentAnalysis
{
    Task<string> AnalyzeSentimentAsync(SentimentDto sentimentDto);
}