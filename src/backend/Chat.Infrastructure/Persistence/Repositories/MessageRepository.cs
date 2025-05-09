using Microsoft.EntityFrameworkCore;
using Chat.Domain.Entities;
using Chat.Domain.Interfaces.Repositories;
using Chat.Infrastructure.Persistence.Data;

namespace Chat.Infrastructure.Persistence.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MessageRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Message>> GetAllByRoomIdAsync(int roomId)
    {
        return await _dbContext.Messages
            .Include(m => m.User)
            .Include(m => m.Sentiment)
            .Where(m => m.RoomId == roomId)
            .ToListAsync();
    }

    public async Task CreateAsync(Message message)
    {
        await _dbContext.Messages.AddAsync(message);
    }
}