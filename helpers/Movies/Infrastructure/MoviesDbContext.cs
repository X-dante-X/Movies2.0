using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Reflection;
using System;
using Microsoft.EntityFrameworkCore;
using MoviesCore.Models;
using System.Net.Sockets;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace Infrastructure;

public class MoviesDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Country> Countries { get; set; }
    public DbSet<Gender> Genders { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<LanguageRole> LanguageRoles { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Keyword> Keywords { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<ProductionCompany> ProductionCompanies { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<MovieCast> MovieCasts { get; set; }
    public DbSet<MovieCompany> MovieCompanies { get; set; }
    public DbSet<MovieCrew> MovieCrews { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }
    public DbSet<MovieKeyword> MovieKeywords { get; set; }
    public DbSet<MovieLanguage> MovieLanguages { get; set; }
    public DbSet<ProductionCountry> ProductionCountries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        string connectionString = "server=localhost;port=3306;database=movies;uid=root;password=mysql;";
        var serverVersion = new MySqlServerVersion(new Version(8, 3, 0));

        optionsBuilder.UseMySql(connectionString, serverVersion);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MovieCast>()
            .HasKey(mc => new { mc.MovieId, mc.PersonId, mc.CharacterName, mc.GenderId });

        modelBuilder.Entity<MovieCompany>()
            .HasKey(mc => new { mc.MovieId, mc.CompanyId });

        modelBuilder.Entity<MovieCrew>()
            .HasKey(mc => new { mc.MovieId, mc.PersonId, mc.DepartmentId, mc.Job });

        modelBuilder.Entity<MovieGenre>()
            .HasKey(mg => new { mg.MovieId, mg.GenreId });

        modelBuilder.Entity<MovieKeyword>()
            .HasKey(mk => new { mk.MovieId, mk.KeywordId });

        modelBuilder.Entity<MovieLanguage>()
            .HasKey(ml => new { ml.MovieId, ml.LanguageId, ml.LanguageRoleId });

        modelBuilder.Entity<ProductionCountry>()
            .HasKey(pc => new { pc.MovieId, pc.CountryId });



        var user = new IdentityUser("yevhenii")
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "yevhenii",
            NormalizedUserName = "YEVHENII",
            Email = "yevhenii.solomchenko@wsei.edu.pl",
            NormalizedEmail = "YEVHENII.SOLOMCHENKO@WSEI.EDU.PL",
            EmailConfirmed = true,
        };

        PasswordHasher<IdentityUser> passwordHasher = new PasswordHasher<IdentityUser>();
        user.PasswordHash = passwordHasher.HashPassword(user, "QWE123qaz!");
        modelBuilder.Entity<IdentityUser>().HasData(user);


    }

}

