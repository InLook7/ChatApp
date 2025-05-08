using Chat.Domain.Entities;
using Chat.Domain.Interfaces.Repositories;
using Chat.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetByIdAsync(int? id)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByUserNameAsync(string userName)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
    }

    public async Task CreateAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }
}