using System;
using System.ComponentModel.DataAnnotations;
using UserService.Models.Enums;

public class UserMovie
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int MovieId { get; set; }
    public bool IsFavorite { get; set; }
    public WatchStatus? Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
