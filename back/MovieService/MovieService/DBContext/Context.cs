using Microsoft.EntityFrameworkCore;
using Models;
using Tag = Models.Tag;


namespace DBContext;

public class Context(DbContextOptions<Context> options) : DbContext(options)
{
    public DbSet<Country> Countries { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<MovieCast> MovieCasts { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<ProductionCompany> ProductionCompanies { get; set; }
    public DbSet<Movie> Movies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //Add-Migration InitalCreate
        //Update-Database
        string connectionString = Environment.GetEnvironmentVariable("POSTGESQL")!;
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MovieCast>()
            .HasKey(mc => new { mc.MovieId, mc.PersonId });
    }
}
