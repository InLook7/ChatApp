using Asp.Versioning;
using Chat.Api.Endpoints;
using Chat.Api.Hubs;
using Chat.Api.Middleware;
using Chat.Application.Extensions;
using Chat.Infrastructure.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer(builder.Configuration);

builder.Services.AddSignalR();

builder.Services.AddApiVersioning(options =>
{
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});

builder.Services.AddOpenApi();

builder.Services.AddCors();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

var versionedGroup = app.NewVersionedApi()
    .MapGroup("api/v{version:apiVersion}")
    .HasApiVersion(1);

versionedGroup.MapRoomEndpoints();

app.MapHub<ChatHub>("chat");

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.Run();
