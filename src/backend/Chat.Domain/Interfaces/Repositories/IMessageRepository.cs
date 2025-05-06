using Chat.Domain.Entities;

namespace Chat.Domain.Interfaces.Repositories;

public interface IMessageRepository
{
    Task<IEnumerable<Message>> GetAllByRoomIdAsync(int roomId);

    Task CreateAsync(Message message); 
}