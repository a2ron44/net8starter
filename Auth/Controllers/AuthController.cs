using Microsoft.AspNetCore.Mvc;
using Net8StarterAuthApi.Auth.Models;
using Net8StarterAuthApi.Auth.Services;

namespace Net8StarterAuthApi.Auth.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly CognitoAuthService _cognitoService;

    public AuthController(CognitoAuthService cognitoService)
    {
        _cognitoService = cognitoService;
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthTokenBundle), 200)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var response = await _cognitoService.Login(request.Email, request.Password);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpPost("refreshToken")]
    [ProducesResponseType(typeof(AuthTokenBundle), 200)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        try
        {
            var response = await _cognitoService.RefreshToken(request.Email, request.RefreshToken);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }
    
    // [HttpPost("signup")]
    // [ProducesResponseType(typeof(AuthTokenBundle), 200)]
    // public async Task<IActionResult> SignupUser([FromBody] SignupRequest request)
    // {
    //     try
    //     {
    //         var response = await _cognitoService.SignUpAsync(request.Email);
    //         return Ok(response);
    //     }
    //     catch (Exception ex)
    //     {
    //         return BadRequest(new { ex.Message });
    //     }
    // }
    
    [HttpPost("admin/createUser")]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<IActionResult> CreateUser([FromBody] SignupRequest request)
    {
        try
        {
            var response = await _cognitoService.AdminCreateUser(request.Email);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }
    
    [HttpPost("admin/userAdminRole")]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<IActionResult> AddUserToAdminRole([FromBody] AddUserAdminRoleRequest request)
    {
        try
        {
            var response = await _cognitoService.AdminAddUserToGroup(request.Email, request.AdminRole);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }
}