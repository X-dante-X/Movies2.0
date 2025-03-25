using AuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //Add-Migration InitalCreate
        //Update-Database
        string connectionString = Environment.GetEnvironmentVariable("POSTGESQL")!;
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql(connectionString);
    }
}
