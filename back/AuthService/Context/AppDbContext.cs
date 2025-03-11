using AuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}
