using MvcMovie.Models;

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; } 
    public string? FullName { get; set; }
    public string? Phone { get; set; }
    public decimal Balance { get; set; }
    public bool IsAdmin { get; set; }
}
