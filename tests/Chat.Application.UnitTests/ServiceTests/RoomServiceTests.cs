using Chat.Application.Interfaces;
using Chat.Application.Services;
using Chat.Domain.Entities;
using Chat.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Chat.Application.UnitTests.ServiceTests;

public class RoomServiceTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RoomService> _logger;
    private readonly IRoomService _roomService;

    public RoomServiceTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _logger = Substitute.For<ILogger<RoomService>>();

        _roomService = new RoomService(_unitOfWork, _logger);
    }

    [Fact]
    public async Task GetAllAsync_GetAllRooms_ReturnsRoomDtos()
    {
        // Arrange
         var rooms = new List<Room>
        {
            new Room() { Id = 1, Name = "TestName1" },
            new Room() { Id = 2, Name = "TestName1" },
            new Room() { Id = 3, Name = "TestName1" }
        };


        _unitOfWork.RoomRepository.GetAllAsync()
            .Returns(rooms);

        // Act
        var result = await _roomService.GetAllAsync();

        // Assert
        await _unitOfWork.RoomRepository.Received(1).GetAllAsync();

        Assert.NotNull(rooms);
        Assert.Equal(rooms.Count(), rooms.Count());
    }
}