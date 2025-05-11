using Chat.Application.Dtos;
using Chat.Domain.Entities;

namespace Chat.Application.Mappers;

public static class MessageMapper
{
    public static MessageDto ToMessageDto(this Message message)
    {
        return new MessageDto
        {
            Id = message.Id,
            Content = message.Content,
            CreatedAt = message.CreatedAt,
            RoomId = message.RoomId,
            UserId = message.UserId,
            UserName = message.User?.UserName,
            Sentiment = message.Sentiment?.SentimentResult
        };
    }

    public static IEnumerable<MessageDto> ToMessageDtos(this IEnumerable<Message> messages)
    {
        return messages.Select(ToMessageDto).ToList();
    }
}