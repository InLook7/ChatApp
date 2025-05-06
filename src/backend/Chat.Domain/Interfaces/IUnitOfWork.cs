using Chat.Domain.Interfaces.Repositories;

namespace Chat.Domain.Interfaces;

public interface IUnitOfWork : IAsyncDisposable
{
    IMessageRepository MessageRepository { get; }
    IRoomRepository RoomRepository { get; }
    IUserRepository UserRepository { get; }

    Task SaveAsync();
}