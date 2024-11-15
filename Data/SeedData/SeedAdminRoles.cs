using Microsoft.EntityFrameworkCore;
using Net8StarterAuthApi.Models;

namespace Net8StarterAuthApi.Data.SeedData;

public static class SeedUserServiceData
{
    public static void SeedAdminRoles(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminRole>().HasData(
            new AdminRole { Name = "sysadmin", Description = "System Admin" },
            new AdminRole { Name = "support", Description = "Internal Support" }
        );
    }
    
    public static void SeedUsers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = Guid.Parse("12d1ab10-aaa6-11ef-9084-ec8f097e5f62"), Email = "a2ron44@gmail.com", FirstName = "Aaron",
                LastName = "H"
            }
        );
    }
    
}

