using System.Net.Http.Json;
using Chat.Common.Responses;
using Chat.Web.Api.Interfaces;

namespace Chat.Web.Api.Services;

public class RoomApiService : IRoomApiService
{
    private readonly HttpClient _httpClient;

    public RoomApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<RoomModel>> GetAllRooms()
    {
        var rooms = await _httpClient.GetFromJsonAsync<List<RoomModel>>("api/v1/rooms");

        return rooms;
    }
}