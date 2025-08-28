
using comerciales.Application.Aut;
using comerciales.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace comerciales.WebApi.Endpoints;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IUserService userService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto request)
    {
        var response = await userService.AuthenticateAsync(request.Email, request.Password);
        return Ok(response);
    }
}
