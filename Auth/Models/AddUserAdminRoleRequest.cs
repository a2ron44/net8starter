namespace Net8StarterAuthApi.Auth.Models;

public class AddUserAdminRoleRequest
{
    public required string Email { get; set; }

    public required string AdminRole { get; set; }
}