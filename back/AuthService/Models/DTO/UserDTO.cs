﻿namespace AuthService.Models.DTO
{
    public class UserDTO
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int UserStatus { get; set; } = 0;
    }
}
