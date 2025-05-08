using Chat.Common.Responses;

namespace Chat.Api.Hubs;

public interface IChatHubClient
{
    Task ReceiveMessage(MessageModel messageModel);
    Task ReceiveError(List<string> errors);
}