using Chat.Common.Responses;

namespace Chat.Web.Api.Interfaces;

public interface IMessageApiService
{
    Task<List<MessageModel>> GetMessagesByRoomId(int roomId);
}