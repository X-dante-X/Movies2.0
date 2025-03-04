using Microsoft.EntityFrameworkCore;
using Models;

namespace GraphQL_DEMO.DBContext;

public class Context : DbContext
{
    public DbSet<Country> Countries { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<MovieCast> MovieCasts { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Models.Tag> Keywords { get; set; }
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
