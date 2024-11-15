using Microsoft.EntityFrameworkCore;
using Net8StarterAuthApi.Constants;
using Net8StarterAuthApi.Data;
using Net8StarterAuthApi.Models;
using InvalidDataException = System.IO.InvalidDataException;

namespace Net8StarterAuthApi.Services;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly ApiDbContext _context;

    public UserService(ILogger<UserService> logger, ApiDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IEnumerable<AdminRole>> GetAllAdminRoles()
    {
        return await _context.AdminRoles.AsNoTracking().ToListAsync();
    }

    public async Task<AdminRole?> GetAdminRoleByName(string name)
    {
        return await _context.AdminRoles.AsNoTracking().FirstOrDefaultAsync(r => r.Name == name);
    }

    public async Task<AdminRole?> AddAdminRole(AdminRole adminRole)
    {
        _context.AdminRoles.Add(adminRole);
        await _context.SaveChangesAsync();
        return adminRole;
    }

    public async Task<bool> UpdateAdminRole(string name, AdminRole adminRole)
    {
        try
        {
            var mod = await GetAdminRoleByName(name);

            if (mod == null)
            {
                throw new NotFoundException();
            }

            if (name != mod.Name)
            {
                _logger.LogInformation("Invalid Name: {name}", name);
                throw new InvalidDataException(ErrorMessages.BadData);
            }
            
            _context.AdminRoles.Update(adminRole);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            throw new ServiceException();
        }
    }

    public async Task<bool> DeleteAdminRole(string name)
    {
        var obj = await GetAdminRoleByName(name);
        if (obj == null)
        {
            throw new NotFoundException();
        }
        _context.AdminRoles.Remove(obj);
        await _context.SaveChangesAsync();
        return true;
    }
}