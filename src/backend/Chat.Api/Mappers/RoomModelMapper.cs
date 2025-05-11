using Chat.Application.Dtos;
using Chat.Common.Responses;

namespace Chat.Api.Mappers;

public static class RoomModelMapper
{
    public static RoomModel ToRoomModel(this RoomDto roomDto)
    {
        return new RoomModel
        {
            Id = roomDto.Id,
            Name = roomDto.Name,
        };
    }

    public static IEnumerable<RoomModel> ToRoomModels(this IEnumerable<RoomDto> roomDtos)
    {
        return roomDtos.Select(ToRoomModel).ToList();
    }
}