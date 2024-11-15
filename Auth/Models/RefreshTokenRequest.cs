namespace Net8StarterAuthApi.Auth.Models;

public class RefreshTokenRequest
{
    public required string RefreshToken { get; set; }

    public required string Email { get; set; }
}