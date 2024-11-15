using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Net8StarterAuthApi.Controllers;

[Authorize]
[Route("api/auth/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return "yes";
    }
}