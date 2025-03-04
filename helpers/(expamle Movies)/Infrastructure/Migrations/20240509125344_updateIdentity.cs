using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00f6cd83-980e-479e-a5fb-003be344752d");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a2d12f7c-38be-412b-a416-fa4e37510703", 0, "232ec271-2283-44f2-a0eb-cdc03af1fb42", "yevhenii.solomchenko@wsei.edu.pl", true, false, null, "YEVHENII.SOLOMCHENKO@WSEI.EDU.PL", "YEVHENII", "AQAAAAIAAYagAAAAEOfP5Q4HB4yi7ShDeyX83wG2kfVndahnzhWUdB0NzLW0RZskuQPEaGlEqF46zAiTOA==", null, false, "a72ae285-3900-4750-af00-fc2e5fa7f7b6", false, "yevhenii" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2d12f7c-38be-412b-a416-fa4e37510703");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "00f6cd83-980e-479e-a5fb-003be344752d", 0, "b78b977f-cba0-431e-81d0-265e9b5a88f6", "yevhenii.solomchenko@wsei.edu.pl", true, false, null, null, null, "AQAAAAIAAYagAAAAEGKeBBr61IN/LAKAYgwrHLCAVRwLmoHHTAbOOD0i8S4CU4DIK1rgs5HB8phFUVNWSw==", null, false, "01105829-5e18-4fea-82db-e43e52c1f0a5", false, "yevhenii" });
        }
    }
}
