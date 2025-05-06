using Chat.Application.Dtos;
using FluentResults;

namespace Chat.Application.Interfaces;

public interface IMessageService
{
    Task<Result<IEnumerable<MessageDto>>> GetAllByRoomIdAsync(int roomId);

    Task<MessageDto> CreateAsync(MessageDto messageDto);
}