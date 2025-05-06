using Chat.Domain.Entities;
using Chat.Infrastructure.Persistence.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Persistence.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Message> Messages { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<User> Users { get; set; }

    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MessageConfiguration());
        modelBuilder.ApplyConfiguration(new RoomConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}