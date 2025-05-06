using Microsoft.EntityFrameworkCore;
using Chat.Domain.Entities;
using Chat.Domain.Interfaces.Repositories;
using Chat.Infrastructure.Persistence.Data;

namespace Chat.Infrastructure.Persistence.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly ApplicationDbContext _dbContext;

    public RoomRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Room>> GetAllAsync()
    {
        return await _dbContext.Rooms.ToListAsync();
    }
}