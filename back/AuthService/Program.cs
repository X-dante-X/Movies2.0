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

var builder = WebApplication.CreateBuilder(args);

//var googleClientId = builder.Configuration["Google:ClientId"] ??
  //                   Environment.GetEnvironmentVariable("Google__ClientId");
//var googleClientSecret = builder.Configuration["Google:ClientSecret"] ??
  //                      Environment.GetEnvironmentVariable("Google__ClientSecret");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"]!);

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
var googleClientSecret = "test";
var googleClientId = "sdasdas";
if (!string.IsNullOrEmpty(googleClientId) && !string.IsNullOrEmpty(googleClientSecret))
{
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
        options.ClientId = googleClientId;
        options.ClientSecret = googleClientSecret;
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.CallbackPath = "/google/callback";

        options.CorrelationCookie.SameSite = SameSiteMode.Lax;
        options.CorrelationCookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;

        options.Events.OnRedirectToAuthorizationEndpoint = context =>
        {
            try
            {
                var uri = new Uri(context.RedirectUri);
                var query = System.Web.HttpUtility.ParseQueryString(uri.Query);

                var redirectUri = query["redirect_uri"];
                if (redirectUri != null)
                {
                    var newRedirectUri = redirectUri
                        .Replace("http://auth_service/google/callback", "http://localhost/auth/google/callback")
                        .Replace("http://authservice/google/callback", "http://localhost/auth/google/callback");

                    query["redirect_uri"] = newRedirectUri;

                    var uriBuilder = new UriBuilder(uri)
                    {
                        Query = query.ToString()
                    };

                    var finalUri = uriBuilder.ToString();

                    Console.WriteLine($"Original redirect_uri: {redirectUri}");
                    Console.WriteLine($"Fixed redirect_uri: {newRedirectUri}");
                    Console.WriteLine($"Final URI: {finalUri}");

                    context.Response.Redirect(finalUri);
                    return Task.CompletedTask;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in redirect: {ex.Message}");
            }

            return Task.CompletedTask;
        };

        options.Events.OnRemoteFailure = context =>
        {
            if (context.Failure?.Message.Contains("Correlation failed") == true ||
                context.Failure?.Message.Contains("oauth state was missing") == true)
            {
                Console.WriteLine($"OAuth remote failure: {context.Failure.Message}");
                Console.WriteLine("Redirecting back to login");
                context.SkipHandler();
                context.Response.Redirect("/google/login");
                return Task.CompletedTask;
            }

            Console.WriteLine($"Unhandled OAuth failure: {context.Failure?.Message}");
            return Task.CompletedTask;
        };

        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.Scope.Add("email");
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
}
else
{
    Console.WriteLine("Google OAuth credentials not found. Only JWT authentication available.");

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
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
}

builder.Services.AddAuthorization();

var app = builder.Build();

// Use forwarded headers BEFORE other middleware
app.UseForwardedHeaders();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseHttpsRedirection();
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