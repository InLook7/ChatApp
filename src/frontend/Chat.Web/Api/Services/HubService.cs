using Chat.Common.Requests;
using Chat.Common.Responses;
using Chat.Web.Api.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;

namespace Chat.Web.Api.Services;

public class HubService : IHubService
{
    private readonly HubConnection _hubConnection;

    public HubService(HubConnection hubConnection)
    {
        _hubConnection = hubConnection;
    }

    public async Task StartAsync()
    {
        if(_hubConnection.State == HubConnectionState.Disconnected)
        {
            await _hubConnection.StartAsync();
        }
    }

    public async Task StopAsync()
    {
        if(_hubConnection.State == HubConnectionState.Connected)
        {
            await _hubConnection.StopAsync();
        }
    }

    public async Task JoinChatAsync(int roomId)
    {
        await _hubConnection.SendAsync("JoinChat", roomId);
    }

    public async Task LeaveChatAsync(int roomId)
    {
        await _hubConnection.SendAsync("LeaveChat", roomId);
    }

    public async Task SendMessageAsync(SendMessageRequest message)
    {
        if (!string.IsNullOrEmpty(message.Content))
        {
            await _hubConnection.SendAsync("SendMessage", message);
        }
    }

    public void OnReceiveMessage(Action<MessageModel> handler)
    {
        _hubConnection.On("ReceiveMessage", handler);
    }

    public void OnReceiveError(Action<List<string>> handler)
    {
        _hubConnection.On("ReceiveError", handler);
    }
}