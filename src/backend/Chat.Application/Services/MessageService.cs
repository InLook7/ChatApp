using Chat.Application.Dtos;
using Chat.Application.Interfaces;
using Chat.Application.Mappers;
using Chat.Domain.Entities;
using Chat.Domain.Interfaces;
using FluentResults;

namespace Chat.Application.Services;

public class MessageService : IMessageService
{
    private readonly IUnitOfWork _unitOfWork;

    public MessageService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<MessageDto>>> GetAllByRoomIdAsync(int roomId)
    {
        var room = await _unitOfWork.RoomRepository.GetByIdAsync(roomId);
        if (room == null)
        {
            return Result.Fail($"Room {roomId} does not exist");
        }

        var messages = await _unitOfWork.MessageRepository.GetAllByRoomIdAsync(roomId);

        return Result.Ok(MessageMapper.ToMessageDto(messages));
    }

    public async Task<MessageDto> CreateAsync(MessageDto messageDto)
    {
        var message = new Message
        {
            Content = messageDto.Content,
            CreatedAt = messageDto.CreatedAt,
            RoomId = messageDto.RoomId,
            UserId = messageDto.UserId
        };

        await _unitOfWork.MessageRepository.CreateAsync(message);
        await _unitOfWork.SaveAsync();

        return MessageMapper.ToMessageDto(message);
    }
}