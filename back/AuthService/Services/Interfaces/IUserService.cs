using AuthService.Models;
using AuthService.Models.DTO;

namespace AuthService.Services.Interfaces;

public interface IUserService
{
    Task<LoginResponseModel> Register(UserDTO userDto);
    Task<LoginResponseModel> Login(LoginRequestModel loginDto);
    Task<TokenResponse> RefreshToken(string accessToken, string refreshToken);
}
