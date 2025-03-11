﻿namespace AuthService.Models.DTO;

public class UserDTO
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int UserStatus { get; set; } = 0;
}
