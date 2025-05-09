using Chat.Domain.Entities;

namespace Chat.Domain.Interfaces.Repositories;

public interface ISentimentRepository
{
    Task CreateAsync(Sentiment sentiment);
}