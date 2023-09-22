using FoodieLionApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodieLionApi.Models;

public class FoodieLionDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<EmailCode> EmailCodes { get; set; }

    public DbSet<Dish> Dishes { get; set; }

    public DbSet<Window> Windows { get; set; }

    public DbSet<Notification> Notifications { get; set; }

    public DbSet<Post> Posts { get; set; }

    public FoodieLionDbContext(DbContextOptions<FoodieLionDbContext> options)
        : base(options) { }
}
