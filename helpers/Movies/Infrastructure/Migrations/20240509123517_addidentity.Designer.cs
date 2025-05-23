﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(MoviesDbContext))]
    [Migration("20240509123517_addidentity")]
    partial class addidentity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "00f6cd83-980e-479e-a5fb-003be344752d",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "b78b977f-cba0-431e-81d0-265e9b5a88f6",
                            Email = "yevhenii.solomchenko@wsei.edu.pl",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            PasswordHash = "AQAAAAIAAYagAAAAEGKeBBr61IN/LAKAYgwrHLCAVRwLmoHHTAbOOD0i8S4CU4DIK1rgs5HB8phFUVNWSw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "01105829-5e18-4fea-82db-e43e52c1f0a5",
                            TwoFactorEnabled = false,
                            UserName = "yevhenii"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("MoviesCore.Models.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("CountryId"));

                    b.Property<string>("CountryIsoCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("CountryId");

                    b.ToTable("country");
                });

            modelBuilder.Entity("MoviesCore.Models.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("DepartmentId"));

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("DepartmentId");

                    b.ToTable("department");
                });

            modelBuilder.Entity("MoviesCore.Models.Gender", b =>
                {
                    b.Property<int>("GenderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("GenderId"));

                    b.Property<string>("GenderName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("GenderId");

                    b.ToTable("gender");
                });

            modelBuilder.Entity("MoviesCore.Models.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("GenreId"));

                    b.Property<string>("GenreName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("GenreId");

                    b.ToTable("genre");
                });

            modelBuilder.Entity("MoviesCore.Models.Keyword", b =>
                {
                    b.Property<int>("KeywordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("KeywordId"));

                    b.Property<string>("KeywordName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("KeywordId");

                    b.ToTable("keyword");
                });

            modelBuilder.Entity("MoviesCore.Models.Language", b =>
                {
                    b.Property<int>("LanguageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("LanguageId"));

                    b.Property<string>("LanguageCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LanguageName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("LanguageId");

                    b.ToTable("language");
                });

            modelBuilder.Entity("MoviesCore.Models.LanguageRole", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("LanguageRoleName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("RoleId");

                    b.ToTable("language_role");
                });

            modelBuilder.Entity("MoviesCore.Models.Movie", b =>
                {
                    b.Property<int>("MovieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("MovieId"));

                    b.Property<int?>("Budget")
                        .HasColumnType("int");

                    b.Property<string>("Homepage")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MovieStatus")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Overview")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal?>("Popularity")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime?>("ReleaseDate")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("Revenue")
                        .HasColumnType("bigint");

                    b.Property<int?>("Runtime")
                        .HasColumnType("int");

                    b.Property<string>("Tagline")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal?>("VoteAverage")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int?>("VoteCount")
                        .HasColumnType("int");

                    b.HasKey("MovieId");

                    b.ToTable("movie");
                });

            modelBuilder.Entity("MoviesCore.Models.MovieCast", b =>
                {
                    b.Property<int?>("MovieId")
                        .HasColumnType("int");

                    b.Property<int?>("PersonId")
                        .HasColumnType("int");

                    b.Property<string>("CharacterName")
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("GenderId")
                        .HasColumnType("int");

                    b.Property<int?>("CastOrder")
                        .HasColumnType("int");

                    b.HasKey("MovieId", "PersonId", "CharacterName", "GenderId");

                    b.HasIndex("GenderId");

                    b.HasIndex("PersonId");

                    b.ToTable("movie_cast");
                });

            modelBuilder.Entity("MoviesCore.Models.MovieCompany", b =>
                {
                    b.Property<int?>("MovieId")
                        .HasColumnType("int");

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.HasKey("MovieId", "CompanyId");

                    b.HasIndex("CompanyId");

                    b.ToTable("movie_company");
                });

            modelBuilder.Entity("MoviesCore.Models.MovieCrew", b =>
                {
                    b.Property<int?>("MovieId")
                        .HasColumnType("int");

                    b.Property<int?>("PersonId")
                        .HasColumnType("int");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Job")
                        .HasColumnType("varchar(255)");

                    b.HasKey("MovieId", "PersonId", "DepartmentId", "Job");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("PersonId");

                    b.ToTable("movie_crew");
                });

            modelBuilder.Entity("MoviesCore.Models.MovieGenre", b =>
                {
                    b.Property<int?>("MovieId")
                        .HasColumnType("int");

                    b.Property<int?>("GenreId")
                        .HasColumnType("int");

                    b.HasKey("MovieId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("movie_genres");
                });

            modelBuilder.Entity("MoviesCore.Models.MovieKeyword", b =>
                {
                    b.Property<int?>("MovieId")
                        .HasColumnType("int");

                    b.Property<int?>("KeywordId")
                        .HasColumnType("int");

                    b.HasKey("MovieId", "KeywordId");

                    b.HasIndex("KeywordId");

                    b.ToTable("movie_keywords");
                });

            modelBuilder.Entity("MoviesCore.Models.MovieLanguage", b =>
                {
                    b.Property<int?>("MovieId")
                        .HasColumnType("int");

                    b.Property<int?>("LanguageId")
                        .HasColumnType("int");

                    b.Property<int?>("LanguageRoleId")
                        .HasColumnType("int");

                    b.HasKey("MovieId", "LanguageId", "LanguageRoleId");

                    b.HasIndex("LanguageId");

                    b.HasIndex("LanguageRoleId");

                    b.ToTable("movie_languages");
                });

            modelBuilder.Entity("MoviesCore.Models.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("PersonId"));

                    b.Property<string>("PersonName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("PersonId");

                    b.ToTable("person");
                });

            modelBuilder.Entity("MoviesCore.Models.ProductionCompany", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("CompanyId"));

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("CompanyId");

                    b.ToTable("production_company");
                });

            modelBuilder.Entity("MoviesCore.Models.ProductionCountry", b =>
                {
                    b.Property<int?>("MovieId")
                        .HasColumnType("int");

                    b.Property<int?>("CountryId")
                        .HasColumnType("int");

                    b.HasKey("MovieId", "CountryId");

                    b.HasIndex("CountryId");

                    b.ToTable("production_country");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MoviesCore.Models.MovieCast", b =>
                {
                    b.HasOne("MoviesCore.Models.Gender", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoviesCore.Models.Movie", "Movie")
                        .WithMany("MovieCasts")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoviesCore.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gender");

                    b.Navigation("Movie");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("MoviesCore.Models.MovieCompany", b =>
                {
                    b.HasOne("MoviesCore.Models.ProductionCompany", "ProductionCompany")
                        .WithMany("MovieCompanies")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoviesCore.Models.Movie", "Movie")
                        .WithMany("MovieCompanies")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("ProductionCompany");
                });

            modelBuilder.Entity("MoviesCore.Models.MovieCrew", b =>
                {
                    b.HasOne("MoviesCore.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoviesCore.Models.Movie", "Movie")
                        .WithMany("MovieCrews")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoviesCore.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Movie");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("MoviesCore.Models.MovieGenre", b =>
                {
                    b.HasOne("MoviesCore.Models.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoviesCore.Models.Movie", "Movie")
                        .WithMany("MovieGenres")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MoviesCore.Models.MovieKeyword", b =>
                {
                    b.HasOne("MoviesCore.Models.Keyword", "Keyword")
                        .WithMany()
                        .HasForeignKey("KeywordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoviesCore.Models.Movie", "Movie")
                        .WithMany("MovieKeywords")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Keyword");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MoviesCore.Models.MovieLanguage", b =>
                {
                    b.HasOne("MoviesCore.Models.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoviesCore.Models.LanguageRole", "LanguageRole")
                        .WithMany()
                        .HasForeignKey("LanguageRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoviesCore.Models.Movie", "Movie")
                        .WithMany("MovieLanguages")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");

                    b.Navigation("LanguageRole");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MoviesCore.Models.ProductionCountry", b =>
                {
                    b.HasOne("MoviesCore.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoviesCore.Models.Movie", "Movie")
                        .WithMany("ProductionCountries")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MoviesCore.Models.Movie", b =>
                {
                    b.Navigation("MovieCasts");

                    b.Navigation("MovieCompanies");

                    b.Navigation("MovieCrews");

                    b.Navigation("MovieGenres");

                    b.Navigation("MovieKeywords");

                    b.Navigation("MovieLanguages");

                    b.Navigation("ProductionCountries");
                });

            modelBuilder.Entity("MoviesCore.Models.ProductionCompany", b =>
                {
                    b.Navigation("MovieCompanies");
                });
#pragma warning restore 612, 618
        }
    }
}
