using Chat.Domain.Entities;
using Chat.Domain.Interfaces.Repositories;
using Chat.Infrastructure.Persistence.Data;

namespace Chat.Infrastructure.Persistence.Repositories;

public class SentimentRepository : ISentimentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public SentimentRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(Sentiment sentiment)
    {
        await _dbContext.Sentiments.AddAsync(sentiment);
    }
}