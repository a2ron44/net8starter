using Microsoft.EntityFrameworkCore;
using Net8StarterAuthApi.Data.SeedData;
using Net8StarterAuthApi.Models;

namespace Net8StarterAuthApi.Data;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Add seed data
        SeedUserServiceData.SeedAdminRoles(modelBuilder);
        SeedUserServiceData.SeedUsers(modelBuilder);
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<AdminRole> AdminRoles { get; set; }
    
    
}