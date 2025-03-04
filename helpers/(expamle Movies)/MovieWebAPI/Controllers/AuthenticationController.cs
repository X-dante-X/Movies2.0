using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebApi.Configuration;
namespace MovieWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtSettings _jwtSettings;

    public AuthenticationController(UserManager<IdentityUser> userManager, JwtSettings jwtSettings)
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Authenticate([FromBody] LoginUserDto user)
    {
        if (!ModelState.IsValid)
        {
            return Unauthorized();
        }
        var logged = await _userManager.FindByNameAsync(user.UserName);
        if (await _userManager.CheckPasswordAsync(logged, user.Password))
        {
            return Ok(new { Token = CreateToken(logged) });
        }
        return Unauthorized();
    }

    private string CreateToken(IdentityUser user)
    {
        return new JwtBuilder()
            .WithAlgorithm(new HMACSHA256Algorithm())
            .WithSecret(Encoding.UTF8.GetBytes(_jwtSettings.Secret))
            .AddClaim(JwtRegisteredClaimNames.Name, user.UserName)
            .AddClaim(JwtRegisteredClaimNames.Email, user.Email)
            .AddClaim(JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.AddMinutes(30).ToUnixTimeSeconds())
            .AddClaim(JwtRegisteredClaimNames.Jti, Guid.NewGuid())
            .Audience(_jwtSettings.Audience)
            .Issuer(_jwtSettings.Issuer)
            .Encode();
    }
}

public class LoginUserDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
