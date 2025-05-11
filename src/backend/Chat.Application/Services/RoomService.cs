using Chat.Application.Dtos;
using Chat.Application.Interfaces;
using Chat.Application.Mappers;
using Chat.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Chat.Application.Services;

public class RoomService : IRoomService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RoomService> _logger;

    public RoomService(
        IUnitOfWork unitOfWork,
        ILogger<RoomService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<IEnumerable<RoomDto>> GetAllAsync()
    {
        _logger.LogInformation("Getting rooms...");

        var rooms = await _unitOfWork.RoomRepository.GetAllAsync();

        _logger.LogInformation("Successfully retrieved {roomCount} rooms.", rooms.Count());
        return rooms.ToRoomDtos(); 
    }
}