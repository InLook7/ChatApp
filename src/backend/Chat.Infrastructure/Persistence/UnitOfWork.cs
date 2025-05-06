using Chat.Domain.Interfaces;
using Chat.Domain.Interfaces.Repositories;
using Chat.Infrastructure.Persistence.Data;
using Chat.Infrastructure.Persistence.Repositories;

namespace Chat.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IMessageRepository MessageRepository => new MessageRepository(_dbContext);
    public IRoomRepository RoomRepository => new RoomRepository(_dbContext);
    public IUserRepository UserRepository => new UserRepository(_dbContext);

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await _dbContext.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}