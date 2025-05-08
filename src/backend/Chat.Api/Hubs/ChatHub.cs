using Chat.Api.Mappers;
using Chat.Application.Dtos;
using Chat.Application.Interfaces;
using Chat.Common.Requests;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Api.Hubs;

public class ChatHub : Hub<IChatHubClient>
{
    private readonly IMessageService _messageService;

    public ChatHub(IMessageService messageService)
    {
        _messageService = messageService;
    }

    public async Task JoinChat(int roomId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"room_{roomId}");
    }

    public async Task LeaveChat(int roomId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"room_{roomId}");
    }

    public async Task SendMessage(SendMessageRequest request)
    {
        var messageDto = new MessageDto
        {
            Content = request.Content,
            CreatedAt = request.CreatedAt,
            RoomId = request.RoomId,
            UserId = request.UserId
        };

        var result = await _messageService.CreateAsync(messageDto);

        if (result.IsFailed)
        {
            await Clients.Caller
                .ReceiveError(result.Errors.Select(e => e.Message).ToList());
        }
        else
        {
            var response = MessageModelMapper.ToMessageModel(result.Value);

            await Clients.Group($"room_{result.Value.RoomId}")
                .ReceiveMessage(response);
        }
    }
}