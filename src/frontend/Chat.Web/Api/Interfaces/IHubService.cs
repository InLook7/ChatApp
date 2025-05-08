using Chat.Common.Requests;
using Chat.Common.Responses;

namespace Chat.Web.Api.Interfaces;

public interface IHubService
{
    Task StartAsync();

    Task StopAsync();

    Task JoinChatAsync(int roomId);

    Task LeaveChatAsync(int roomId);

    Task SendMessageAsync(SendMessageRequest request);

    void OnReceiveMessage(Action<MessageModel> handler);

    void OnReceiveError(Action<List<string>> handler);
}