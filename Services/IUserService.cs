using Net8StarterAuthApi.Models;

namespace Net8StarterAuthApi.Services;

public interface IUserService
{
    Task<IEnumerable<AdminRole>> GetAllAdminRoles();
    Task<AdminRole?> GetAdminRoleByName(string name);
    Task<AdminRole?> AddAdminRole(AdminRole adminRole);
    Task<bool> UpdateAdminRole(string name, AdminRole adminRole);
    Task<bool> DeleteAdminRole(string name);
}