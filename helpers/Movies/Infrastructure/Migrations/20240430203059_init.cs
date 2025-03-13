using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "country",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CountryIsoCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CountryName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_country", x => x.CountryId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "department",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DepartmentName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_department", x => x.DepartmentId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "gender",
                columns: table => new
                {
                    GenderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GenderName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gender", x => x.GenderId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "genre",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GenreName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genre", x => x.GenreId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "keyword",
                columns: table => new
                {
                    KeywordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    KeywordName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_keyword", x => x.KeywordId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "language",
                columns: table => new
                {
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LanguageCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LanguageName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_language", x => x.LanguageId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "language_role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LanguageRoleName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_language_role", x => x.RoleId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "movie",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Budget = table.Column<int>(type: "int", nullable: true),
                    Homepage = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Overview = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Popularity = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Revenue = table.Column<long>(type: "bigint", nullable: true),
                    Runtime = table.Column<int>(type: "int", nullable: true),
                    MovieStatus = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tagline = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VoteAverage = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    VoteCount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie", x => x.MovieId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "person",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PersonName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person", x => x.PersonId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "production_company",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CompanyName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_production_company", x => x.CompanyId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "movie_genres",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie_genres", x => new { x.MovieId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_movie_genres_genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "genre",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_movie_genres_movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "movie",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "movie_keywords",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    KeywordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie_keywords", x => new { x.MovieId, x.KeywordId });
                    table.ForeignKey(
                        name: "FK_movie_keywords_keyword_KeywordId",
                        column: x => x.KeywordId,
                        principalTable: "keyword",
                        principalColumn: "KeywordId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_movie_keywords_movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "movie",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "movie_languages",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    LanguageRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie_languages", x => new { x.MovieId, x.LanguageId, x.LanguageRoleId });
                    table.ForeignKey(
                        name: "FK_movie_languages_language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "language",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_movie_languages_language_role_LanguageRoleId",
                        column: x => x.LanguageRoleId,
                        principalTable: "language_role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_movie_languages_movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "movie",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "production_country",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_production_country", x => new { x.MovieId, x.CountryId });
                    table.ForeignKey(
                        name: "FK_production_country_country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "country",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_production_country_movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "movie",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "movie_cast",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    CharacterName = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GenderId = table.Column<int>(type: "int", nullable: false),
                    CastOrder = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie_cast", x => new { x.MovieId, x.PersonId, x.CharacterName, x.GenderId });
                    table.ForeignKey(
                        name: "FK_movie_cast_gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "gender",
                        principalColumn: "GenderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_movie_cast_movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "movie",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_movie_cast_person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "movie_crew",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    Job = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie_crew", x => new { x.MovieId, x.PersonId, x.DepartmentId, x.Job });
                    table.ForeignKey(
                        name: "FK_movie_crew_department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "department",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_movie_crew_movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "movie",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_movie_crew_person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "movie_company",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie_company", x => new { x.MovieId, x.CompanyId });
                    table.ForeignKey(
                        name: "FK_movie_company_movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "movie",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_movie_company_production_company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "production_company",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_movie_cast_GenderId",
                table: "movie_cast",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_movie_cast_PersonId",
                table: "movie_cast",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_movie_company_CompanyId",
                table: "movie_company",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_movie_crew_DepartmentId",
                table: "movie_crew",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_movie_crew_PersonId",
                table: "movie_crew",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_movie_genres_GenreId",
                table: "movie_genres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_movie_keywords_KeywordId",
                table: "movie_keywords",
                column: "KeywordId");

            migrationBuilder.CreateIndex(
                name: "IX_movie_languages_LanguageId",
                table: "movie_languages",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_movie_languages_LanguageRoleId",
                table: "movie_languages",
                column: "LanguageRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_production_country_CountryId",
                table: "production_country",
                column: "CountryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "movie_cast");

            migrationBuilder.DropTable(
                name: "movie_company");

            migrationBuilder.DropTable(
                name: "movie_crew");

            migrationBuilder.DropTable(
                name: "movie_genres");

            migrationBuilder.DropTable(
                name: "movie_keywords");

            migrationBuilder.DropTable(
                name: "movie_languages");

            migrationBuilder.DropTable(
                name: "production_country");

            migrationBuilder.DropTable(
                name: "gender");

            migrationBuilder.DropTable(
                name: "production_company");

            migrationBuilder.DropTable(
                name: "department");

            migrationBuilder.DropTable(
                name: "person");

            migrationBuilder.DropTable(
                name: "genre");

            migrationBuilder.DropTable(
                name: "keyword");

            migrationBuilder.DropTable(
                name: "language");

            migrationBuilder.DropTable(
                name: "language_role");

            migrationBuilder.DropTable(
                name: "country");

            migrationBuilder.DropTable(
                name: "movie");
        }
    }
}
