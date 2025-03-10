using AuthService.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

using System.Text;
using AuthService.Services.Interfaces;
using AuthService.Models;
using AuthService.Models.DTO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"]);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase(databaseName: "UserDb"));

builder.Services.AddScoped<IUserService, UserService>();   

builder.Services.AddScoped<IJwtService, JwtService>();  

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});


var app = builder.Build();

app.MapPost("/api/auth/register", async (IUserService userService, UserDTO userDto) =>
{
    try
    {
        var response = await userService.Register(userDto);
        return Results.Ok(response);
    }
    catch (ApplicationException ex)
    {
        return Results.BadRequest(new { message = ex.Message });
    }
});

app.MapPost("/api/auth/login", async (IUserService userService, LoginRequestModel loginDto) =>
{
    try
    {
        var response = await userService.Login(loginDto);
        return Results.Ok(response);
    }
    catch (ApplicationException ex)
    {
        return Results.BadRequest(new { message = ex.Message });
    }
});


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();


    if (!context.Users.Any())
    {

        var adminUser = new User
        {
            Username = "admin",
            Email = "admin@example.com",
            UserStatus = 1,
            RefreshToken = services.GetRequiredService<IJwtService>().GenerateRefreshToken(),
            RefreshTokenExpiry = DateTime.UtcNow.AddDays(7)
        };
        var user = new User
        {
            Username = "user",
            Email = "user@example.com",
            UserStatus = 0,
            RefreshToken = services.GetRequiredService<IJwtService>().GenerateRefreshToken(),
            RefreshTokenExpiry = DateTime.UtcNow.AddDays(7)
        };

        using (var hmac = new System.Security.Cryptography.HMACSHA512())
        {
            adminUser.PasswordSalt = hmac.Key;
            adminUser.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("Anime123!$"));
            user.PasswordSalt = hmac.Key;
            user.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("User@!!$"));
        }

        adminUser.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
        context.Users.Add(adminUser);
        context.Users.Add(user);

        context.SaveChanges();
    }
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();    
app.UseHttpsRedirection();

app.UseAuthorization();

app.Run();
