namespace Net8StarterAuthApi.Auth.Models;

public class LoginRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}