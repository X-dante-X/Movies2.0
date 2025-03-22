using Infrastructure;
using Infrastructure.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MoviesCore.Interfaces;
using Newtonsoft.Json;
using System.Text;
using WebApi.Configuration;


namespace API;
public partial class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = "localhost:6379";
            options.InstanceName = "Movies";
        });

        builder.Services.AddTransient<IUserService, UserService>();
        builder.Services.AddScoped<IInformationService, InformationService>();
        builder.Services.AddTransient<IAdminService, AdminService>();
        builder.Services.AddDbContext<MoviesDbContext>()
            .AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<MoviesDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddSingleton<JwtSettings>();
        builder.Services.ConfigureJWT(new JwtSettings(builder.Configuration));
        builder.Services.ConfigureCors();
        builder.Services.ConfigureSwagger();



        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseCors("CorsPolicy");
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.Run();
    }
}
public partial class Program
{
}
