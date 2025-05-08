using Chat.Domain.Entities;

namespace Chat.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int? id);

    Task<User?> GetByUserNameAsync(string userName);

    Task CreateAsync(User user);
}