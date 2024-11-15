using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Net8StarterAuthApi.Constants;
using Net8StarterAuthApi.Models;
using Net8StarterAuthApi.Models.DTOs;
using Net8StarterAuthApi.Services;
using InvalidDataException = System.IO.InvalidDataException;

namespace Net8StarterAuthApi.Controllers;

[ApiController]
[Route("api/v1/auth/[controller]")]
public class AdminRoleController : ControllerBase
{
    private readonly ILogger<AdminRoleController> _logger;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;


    public AdminRoleController(ILogger<AdminRoleController> logger,IMapper mapper, IUserService userService)
    {
        _logger = logger;
        _mapper = mapper;
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AdminRoleDto>>> GetAdminRoles()
    {
        var result = await _userService.GetAllAdminRoles();
        return Ok(result);
    } 
    
    [HttpGet("{name}")]
    public async Task<ActionResult<AdminRoleDto>> GetAdminRoleByName(string name)
    {
        try
        {
            _logger.LogTrace("GetBy Name: {name}", name);
            var adminRole = await _userService.GetAdminRoleByName(name);

            if (adminRole == null)
            {
                return NotFound();
            }

            return Ok(adminRole);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return BadRequest(ErrorMessages.UnknownError);
        }
    }

    [HttpPut("{name}")]
    public async Task<IActionResult> UpdateAdminRole(string name, AdminRoleDto adminRoleDto)
    {
        try
        {
            _logger.LogTrace("Update ID: {name}", name);

            var obj = _mapper.Map<AdminRole>(adminRoleDto);
            
            var result = await _userService.UpdateAdminRole(name, obj);

            if (!result) return BadRequest(ErrorMessages.ErrorSaving);

            return NoContent();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return BadRequest(ErrorMessages.UnknownError);
        }
    }


    [HttpPost]
    public async Task<ActionResult<AdminRoleDto>> AddAdminRole(AdminRoleDto objDto)
    {
        try
        {
            var obj = _mapper.Map<AdminRole>(objDto);
            var result = await _userService.AddAdminRole(obj);
            if (result == null) return BadRequest(ErrorMessages.ErrorSaving);

            return CreatedAtAction(nameof(GetAdminRoleByName), new { name = result.Name }, result);

        }
        catch (Exception)
        {
            return BadRequest(ErrorMessages.UnknownError);
        }
    }

    [HttpDelete("{name}")]
    public async Task<IActionResult> DeleteAdminRole(string name)
    {
        try
        {
            _logger.LogTrace("Delete Name: {name}", name);

            var result = await _userService.DeleteAdminRole(name);

            if (!result) return BadRequest(ErrorMessages.ErrorSaving);

            return NoContent();
        } catch (NotFoundException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return BadRequest(ErrorMessages.UnknownError);
        }
    }

}
