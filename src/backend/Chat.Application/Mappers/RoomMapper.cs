using Chat.Application.Dtos;
using Chat.Domain.Entities;

namespace Chat.Application.Mappers;

public static class RoomMapper
{
    public static RoomDto ToRoomDto(this Room room)
    {
        return new RoomDto
        {
            Id = room.Id,
            Name = room.Name
        };
    }

    public static IEnumerable<RoomDto> ToRoomDtos(this IEnumerable<Room> rooms)
    {
        return rooms.Select(ToRoomDto).ToList();
    }
}