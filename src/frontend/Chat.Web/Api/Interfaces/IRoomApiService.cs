using Chat.Common.Responses;

namespace Chat.Web.Api.Interfaces;

public interface IRoomApiService
{
    Task<List<RoomModel>> GetAllRooms();
}