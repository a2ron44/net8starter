using System.ComponentModel.DataAnnotations;

namespace Net8StarterAuthApi.Models;

public class AdminRole
{
    [Key] 
    [StringLength(15)]
    public required string Name { get; set; }

    [StringLength(30)]
    public string? Description { get; set; }
    
    public virtual ICollection<User>? Users { get; set; }
}