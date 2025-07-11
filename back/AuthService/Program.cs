using AuthService.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Text;
using AuthService.Services.Interfaces;
using AuthService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpOverrides;
using AuthService.Services;
using AuthService.RabbitMQService;

var builder = WebApplication.CreateBuilder(args);

//var googleClientId = builder.Configuration["Google:ClientId"] ??
  //                   Environment.GetEnvironmentVariable("Google__ClientId");
//var googleClientSecret = builder.Configuration["Google:ClientSecret"] ??
  //                      Environment.GetEnvironmentVariable("Google__ClientSecret");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Configuration.AddUserSecrets<Program>();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"]!);

builder.Services.AddSingleton<RabbitMqService>();
builder.Services.AddHostedService<RabbitMqListenerService>();

builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                              ForwardedHeaders.XForwardedHost |
                              ForwardedHeaders.XForwardedProto;
    options.KnownProxies.Clear();
    options.KnownNetworks.Clear();
});



builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
    })
    .AddCookie(options =>
    {
        options.LoginPath = "/google/login";
        options.Cookie.Name = "AuthService.Auth";
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    })
    .AddGoogle(options =>
    {
        var clientId = "";

        if (clientId == null)
        {
            throw new ArgumentNullException(nameof(clientId));
        }

        var clientSecret = "";

        if (clientSecret == null)
        {
            throw new ArgumentNullException(nameof(clientSecret));
        }

        options.ClientId = "";
        options.ClientSecret = "";
        options.CallbackPath = "/auth/google/callback";
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme; 
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

builder.Services.AddAuthorization();

var app = builder.Build();
app.UsePathBase("/auth");
// Use forwarded headers BEFORE other middleware
app.UseForwardedHeaders();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

// app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.MapAuthEndpoints();
SeedUsers.Seed(app.Services);

app.Run();