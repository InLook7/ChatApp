using Chat.Application.Dtos;

namespace Chat.Application.Interfaces;

public interface IRoomService
{
    Task<IEnumerable<RoomDto>> GetAllAsync();
}