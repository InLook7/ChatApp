using Chat.Application.Dtos;
using Chat.Application.Interfaces;
using Chat.Application.Mappers;
using Chat.Domain.Entities;
using Chat.Domain.Interfaces;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace Chat.Application.Services;

public class MessageService : IMessageService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<MessageService> _logger;

    public MessageService(
        IUnitOfWork unitOfWork,
        ILogger<MessageService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<MessageDto>>> GetAllByRoomIdAsync(int roomId)
    {
        _logger.LogInformation("Getting messages for Room {roomId}...", roomId);

        var room = await _unitOfWork.RoomRepository.GetByIdAsync(roomId);
        if (room == null)
        {
            _logger.LogWarning("Room {roomId} does not exist.", roomId);
            return Result.Fail($"Room {roomId} was not found.");
        }

        var messages = await _unitOfWork.MessageRepository.GetAllByRoomIdAsync(roomId);

        _logger.LogInformation("Successfully retrieved {messageCount} messages.", messages.Count());
        return Result.Ok(MessageMapper.ToMessageDto(messages));
    }

    public async Task<MessageDto> CreateAsync(MessageDto messageDto)
    {
        _logger.LogInformation("Creating a new message by User {userId}...", messageDto.UserId);

        var message = new Message
        {
            Content = messageDto.Content,
            CreatedAt = messageDto.CreatedAt,
            RoomId = messageDto.RoomId,
            UserId = messageDto.UserId
        };

        await _unitOfWork.MessageRepository.CreateAsync(message);
        await _unitOfWork.SaveAsync();

        _logger.LogInformation("Successfully created Message {messageId}.", message.Id);
        return MessageMapper.ToMessageDto(message);
    }
}