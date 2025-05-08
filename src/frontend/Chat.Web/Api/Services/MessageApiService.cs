using System.Net.Http.Json;
using Chat.Common.Responses;
using Chat.Web.Api.Interfaces;

namespace Chat.Web.Api.Services;

public class MessageApiService : IMessageApiService
{
    private readonly HttpClient _httpClient;

    public MessageApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<MessageModel>> GetMessagesByRoomId(int roomId)
    {
        var messages = await _httpClient.GetFromJsonAsync<List<MessageModel>>($"api/v1/rooms/{roomId}/messages");

        return messages;
    }
}