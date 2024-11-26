using System.ComponentModel;

namespace Net8StarterAuthApi.Auth.Models;

public class RefreshTokenRequest
{
    public required string RefreshToken { get; set; }
    
    public required string UserSubId { get; set; }
}