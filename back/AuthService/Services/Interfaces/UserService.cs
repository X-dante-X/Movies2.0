using AuthService.Context;
using AuthService.Models;
using AuthService.Models.DTO;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

using System.Text;

namespace AuthService.Services.Interfaces
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IJwtService _jwtService;
        public UserService(AppDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<LoginResponseModel> Login(LoginRequestModel loginDto)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == loginDto.UserName);
            if (user == null)
                throw new ApplicationException("User not found");

            // Verify password
            if (!VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt))
                throw new ApplicationException("Password is incorrect");

            // Generate JWT token
            var token = _jwtService.GenerateToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

            await _context.SaveChangesAsync();

            return new LoginResponseModel
            {
                UserName = user.Username,
                RefreshToken = refreshToken,
                Expiration = DateTime.UtcNow.AddMinutes(15),
                AccessToken = token,
                IsAdmin = user.IsAdmin
            };
        }

        public async Task<TokenResponse> RefreshToken(string accessToken, string refreshToken)
        {

            var principal = _jwtService.GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity.Name;


            var user = _context.Users.SingleOrDefault(u => u.Username == username);
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiry <= DateTime.UtcNow)
                throw new SecurityTokenException("Invalid refresh token");


            var newAccessToken = _jwtService.GenerateToken(user);
            var newRefreshToken = _jwtService.GenerateRefreshToken();


            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
            await _context.SaveChangesAsync();

            return new TokenResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                Expiration = DateTime.UtcNow.AddMinutes(15)
            };
        }

        public async Task<LoginResponseModel> Register(UserDTO userDto)
        {
            if (_context.Users.Any(u => u.Username == userDto.Username))
                throw new ApplicationException("Username already exists");

            if (_context.Users.Any(u => u.Email == userDto.Email))
                throw new ApplicationException("Email already exists");

            // Create password hash and salt
            CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Username = userDto.Username,
                Email = userDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                UserStatus = 0 // Prevents other people from setting up the admin for themselves
                               // . Only other admins can promote a user to an admin, idk should we use Identity + Roles for that?
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Generate JWT token
            var token = _jwtService.GenerateToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7); 
            await _context.SaveChangesAsync();

            return new LoginResponseModel
            {
                UserName = user.Username,
                AccessToken = token,
                RefreshToken = refreshToken,
                Expiration = DateTime.UtcNow.AddMinutes(15),
                IsAdmin = user.IsAdmin
            };
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
                return true;
            }
        }
    }
}
