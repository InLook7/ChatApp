using Chat.Application.Dtos;
using Chat.Application.Interfaces;
using Chat.Application.Mappers;
using Chat.Domain.Entities;
using Chat.Domain.Interfaces;
using FluentResults;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Chat.Application.Services;

public class MessageService : IMessageService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<MessageService> _logger;
    private readonly IValidator<MessageDto> _validator;

    public MessageService(
        IUnitOfWork unitOfWork,
        ILogger<MessageService> logger,
        IValidator<MessageDto> validator)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _validator = validator;
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

    public async Task<Result<MessageDto>> CreateAsync(MessageDto messageDto)
    {
        _logger.LogInformation("Creating a new message by User {userId}...", messageDto.UserId);

        var validationResult = await _validator.ValidateAsync(messageDto);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed.");
            return Result.Fail(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        var room = await _unitOfWork.RoomRepository.GetByIdAsync(messageDto.RoomId);
        if (room == null)
        {
            _logger.LogWarning("Room {roomId} does not exist.", messageDto.RoomId);
            return Result.Fail($"Room {messageDto.RoomId} was not found.");
        }

        if (messageDto.UserId != null)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(messageDto.UserId);
            if (user == null)
            {
                _logger.LogWarning("User {userId} does not exist.", messageDto.UserId);
                return Result.Fail($"User {messageDto.UserId} was not found.");
            }
        }

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
        return Result.Ok(MessageMapper.ToMessageDto(message));
    }
}