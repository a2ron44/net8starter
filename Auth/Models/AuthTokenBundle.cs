namespace Net8StarterAuthApi.Auth.Models;

public class AuthTokenBundle
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
    public required string TokenType { get; set; }
    public required int ExpiresIn { get; set; }
}