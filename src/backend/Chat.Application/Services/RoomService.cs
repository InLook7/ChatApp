using Chat.Application.Dtos;
using Chat.Application.Interfaces;
using Chat.Application.Mappers;
using Chat.Domain.Interfaces;

namespace Chat.Application.Services;

public class RoomService : IRoomService
{
    private readonly IUnitOfWork _unitOfWork;

    public RoomService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<RoomDto>> GetAllAsync()
    {
        var rooms = await _unitOfWork.RoomRepository.GetAllAsync();

        return RoomMapper.ToRoomDto(rooms); 
    }
}