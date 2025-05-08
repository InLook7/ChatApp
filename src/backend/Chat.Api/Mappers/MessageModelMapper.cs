using Chat.Application.Dtos;
using Chat.Common.Responses;

namespace Chat.Api.Mappers;

public static class MessageModelMapper
{
    public static MessageModel ToMessageModel(MessageDto messageDto)
    {
        return new MessageModel
        {
            Id = messageDto.Id,
            Content = messageDto.Content,
            CreatedAt = messageDto.CreatedAt,
            RoomId = messageDto.RoomId,
            UserId = messageDto.UserId
        };
    }

    public static IEnumerable<MessageModel> ToMessageModel(IEnumerable<MessageDto> messageDtos)
    {
        return messageDtos.Select(ToMessageModel).ToList();
    }
}