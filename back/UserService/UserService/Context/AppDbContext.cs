using Microsoft.EntityFrameworkCore;
using System;
using UserService.Models;

namespace UserService.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<UserMovie> UserMovies { get; set; }
        public DbSet<MovieReview> MovieReviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = Environment.GetEnvironmentVariable("POSTGESQL")!;
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserMovie>()
           .HasIndex(f => new { f.UserId, f.MovieId })
           .IsUnique();

            modelBuilder.Entity<MovieReview>()
                .HasIndex(mr => new { mr.UserId, mr.MovieId })
                .IsUnique();

        }
    }
}
