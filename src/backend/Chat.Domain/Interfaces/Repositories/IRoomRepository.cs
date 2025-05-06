using Chat.Domain.Entities;

namespace Chat.Domain.Interfaces.Repositories;

public interface IRoomRepository
{
    Task<IEnumerable<Room>> GetAllAsync();

    Task<Room?> GetByIdAsync(int id);
}