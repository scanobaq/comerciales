
using comerciales.Application.Aut;
using comerciales.Application.DTOs;
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
        try
        {
            var token = await userService.AuthenticateAsync(request.Email, request.Password);
            var response = new LoginResponseDto { AccessToken = token };
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}
