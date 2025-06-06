using Chat.Api.Mappers;
using Chat.Application.Interfaces;

namespace Chat.Api.Endpoints;

public static class RoomEndpoints
{
    public static void MapRoomEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("rooms");

        group.MapGet("/", GetRooms)
            .MapToApiVersion(1);     

        group.MapGet("/{roomId}/messages", GetMessagesByRoomId)
            .MapToApiVersion(1);
    }

    private static async Task<IResult> GetRooms(IRoomService roomService)
    {
        var rooms = await roomService.GetAllAsync();

        var response = rooms.ToRoomModels();
        return TypedResults.Ok(response);
    }

    private static async Task<IResult> GetMessagesByRoomId(
        IMessageService messageService,
        int roomId)
    {
        var result = await messageService.GetAllByRoomIdAsync(roomId);

        if (result.IsFailed)
        {
            return TypedResults.BadRequest(result.Errors.Select(e => e.Message));
        }

        var response = result.Value.ToMessageModels();
        return TypedResults.Ok(response);
    }
}