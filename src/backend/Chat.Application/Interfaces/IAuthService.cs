using Chat.Application.Dtos;
using FluentResults;

namespace Chat.Application.Interfaces;

public interface IAuthService
{
    Task<Result> RegisterAsync(UserDto userDto);

    Task<Result<string>> LoginAsync(UserDto userDto);
}