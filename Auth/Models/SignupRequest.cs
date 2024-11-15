using System.ComponentModel.DataAnnotations;

namespace Net8StarterAuthApi.Auth.Models;

public class SignupRequest
{
    [EmailAddress]
    public required string Email { get; set; }
}