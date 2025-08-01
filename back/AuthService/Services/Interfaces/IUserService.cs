﻿using AuthService.Models;

namespace AuthService.Services.Interfaces;

public interface IUserService
{
    Task<LoginResponseModel> Register(RegisterRequest userDto);
    Task<LoginResponseModel> Login(LoginRequestModel loginDto);
    Task<TokenResponse> RefreshToken(string accessToken, string refreshToken);
    Task<ValidateResponse> Validate(string token);
    Task<LoginResponseModel> GoogleAuth(GoogleAuthRequest googleAuthDto); 
    Task<User?> GetUserByGoogleIdAsync(string googleId);
    Task<User?> GetUserByEmailAsync(string email); 

    Task<bool> Logout();
}
