using Microsoft.AspNetCore.Mvc;
using Chat.Application.Dtos;
using Chat.Application.Interfaces;
using Chat.Common.Requests;

namespace Chat.Api.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("auth");

        group.MapPost("/login", Login)
            .MapToApiVersion(1);

        group.MapPost("/register", Register)
            .MapToApiVersion(1);
    }

    private static async Task<IResult> Login(
        IAuthService authService,
        [FromBody] UserLoginRequest request)
    {
        var userDto = new UserDto
        {
            UserName = request.UserName,
            Password = request.Password,
        };

        var result = await authService.LoginAsync(userDto);

        if (result.IsFailed)
        {
            return TypedResults.BadRequest(result.Errors.Select(e => e.Message));
        }

        return TypedResults.Ok(result.Value);
    }
    
    private static async Task<IResult> Register(
        IAuthService authService,
        [FromBody] UserRegisterRequest request)
    {
        var userDto = new UserDto
        {
            UserName = request.UserName,
            Password = request.Password,
        };

        var result = await authService.RegisterAsync(userDto);

        if (result.IsFailed)
        {
            return TypedResults.BadRequest(result.Errors.Select(e => e.Message));
        }

        return TypedResults.Ok();
    }
}