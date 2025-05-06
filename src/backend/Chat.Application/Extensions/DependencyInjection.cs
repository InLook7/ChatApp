using Chat.Application.Interfaces;
using Chat.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<IRoomService, RoomService>();

        return services;
    }
}