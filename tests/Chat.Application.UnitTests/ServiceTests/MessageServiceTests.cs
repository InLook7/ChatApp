using Xunit;
using NSubstitute;
using Microsoft.Extensions.Logging;
using FluentValidation;
using FluentValidation.Results;
using Chat.Application.Dtos;
using Chat.Application.Interfaces;
using Chat.Application.Services;
using Chat.Domain.Entities;
using Chat.Domain.Interfaces;

namespace Chat.Application.UnitTests.ServiceTests;

public class MessageServiceTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<MessageService> _logger;
    private readonly IValidator<MessageDto> _validator;
    private readonly IMessageService _messageService;

    public MessageServiceTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _logger = Substitute.For<ILogger<MessageService>>();
        _validator = Substitute.For<IValidator<MessageDto>>();

        _messageService = new MessageService(_unitOfWork, _logger, _validator);
    }

    [Fact]
    public async Task GetAllByRoomIdAsync_GetByExistingRoomId_ReturnsMessages()
    {
        // Arrange
        var room = new Room
        {
            Id = 1,
            Name = "TestRoom"
        };

        var messages = new List<Message>
        {
            new Message() { Content = "Test1", RoomId = room.Id },
            new Message() { Content = "Test2", RoomId = room.Id },
            new Message() { Content = "Test3", RoomId = room.Id }
        };

        _unitOfWork.RoomRepository.GetByIdAsync(room.Id)
            .Returns(room);
        _unitOfWork.MessageRepository.GetAllByRoomIdAsync(room.Id)
            .Returns(messages);

        // Act
        var result = await _messageService.GetAllByRoomIdAsync(room.Id);

        // Assert
        await _unitOfWork.RoomRepository.Received(1).GetByIdAsync(room.Id);

        Assert.NotNull(result.Value);
        Assert.Equal(messages.Count(), result.Value.Count());
    }

    [Fact]
    public async Task GetAllByRoomIdAsync_GetByNotExistingRoomId_ReturnsFail()
    {
        // Arrange
        var notExistingRoomId = 3;

        _unitOfWork.RoomRepository.GetByIdAsync(notExistingRoomId)
            .Returns((Room)null);

        // Act
        var result = await _messageService.GetAllByRoomIdAsync(notExistingRoomId);

        // Assert
        await _unitOfWork.RoomRepository.Received(1).GetByIdAsync(notExistingRoomId);

        Assert.True(result.IsFailed);
    }

    [Fact]
    public async Task CreateAsync_CreateValidMessage_ReturnsSuccessfulProcess()
    {
        // Arrange
        var messageDto = new MessageDto()
        {
            Content = "Hello!",
            CreatedAt = DateTime.UtcNow,
            RoomId = 1,
            UserId = 1,
        };

        _validator.ValidateAsync(Arg.Any<MessageDto>(), CancellationToken.None)
            .Returns(new ValidationResult());
        _unitOfWork.MessageRepository.CreateAsync(Arg.Any<Message>())
            .Returns(Task.CompletedTask);
        _unitOfWork.RoomRepository.GetByIdAsync(messageDto.RoomId)
            .Returns(new Room());
        _unitOfWork.UserRepository.GetByIdAsync(messageDto.UserId)
            .Returns(new User());
        _unitOfWork.SaveAsync()
            .Returns(Task.CompletedTask);

        // Act
        var result = await _messageService.CreateAsync(messageDto);

        // Assert
        await _validator.Received(1).ValidateAsync(Arg.Any<MessageDto>(), CancellationToken.None);
        await _unitOfWork.RoomRepository.Received(1).GetByIdAsync(messageDto.RoomId);
        await _unitOfWork.UserRepository.Received(1).GetByIdAsync(messageDto.UserId);
        await _unitOfWork.MessageRepository.Received(1).CreateAsync(Arg.Any<Message>());
        await _unitOfWork.Received(1).SaveAsync();

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
    }
    
    [Fact]
    public async Task CreateAsync_CreateInvalidMessage_ReturnsSuccessfulProcess()
    {
        // Arrange
        var messageDto = new MessageDto()
        {
            Content = "Hello!",
            CreatedAt = DateTime.UtcNow.AddMinutes(-1),
            RoomId = 1,
            UserId = 1,
        };

        _validator.ValidateAsync(Arg.Any<MessageDto>(), CancellationToken.None)
            .Returns(new ValidationResult());
   
        // Act
        var result = await _messageService.CreateAsync(messageDto);

        // Assert
        await _validator.Received(1).ValidateAsync(Arg.Any<MessageDto>(), CancellationToken.None);

        Assert.True(result.IsFailed);
    }
}