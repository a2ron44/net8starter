using System.ComponentModel.DataAnnotations;

namespace Net8StarterAuthApi.Models;

public class User
{
    public Guid Id { get; set; }

    [StringLength(40)]
    public required string Email { get; set; }

    [StringLength(20)]
    public string? FirstName { get; set; }

    [StringLength(20)]
    public string? LastName { get; set; }
    
    public virtual ICollection<AdminRole>? AdminRoles { get; set; }
}