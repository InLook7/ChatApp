using Chat.Application.Dtos;
using Chat.Common.Responses;

namespace Chat.Api.Mappers;

public static class MessageModelMapper
{
    public static MessageModel ToMessageModel(this MessageDto messageDto)
    {
        return new MessageModel
        {
            Id = messageDto.Id,
            Content = messageDto.Content,
            CreatedAt = messageDto.CreatedAt,
            RoomId = messageDto.RoomId,
            UserId = messageDto.UserId,
            UserName = messageDto.UserName,
            Sentiment = messageDto.Sentiment
        };
    }

    public static IEnumerable<MessageModel> ToMessageModels(this IEnumerable<MessageDto> messageDtos)
    {
        return messageDtos.Select(ToMessageModel).ToList();
    }
}